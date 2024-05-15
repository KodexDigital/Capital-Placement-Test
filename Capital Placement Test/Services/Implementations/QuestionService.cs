using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Enums;
using Capital_Placement_Test.Exceptions;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Capital_Placement_Test.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IDatabaseConnection databaseConnection;
        /// <summary>
        /// Database container name alia Table
        /// </summary>
        protected internal string? Table { get; } = nameof(QuestionType);

        /// <summary>
        /// Unique prorty for getting container items.
        /// This is should not be changes by any means
        /// </summary>
        protected internal string? PartitionKey { get; set; } = "clientKey";
        public QuestionService(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }
        public async Task<Tuple<ResponseHandler<List<QuestionType>>, ErrorResponse>> CreateQuestionTypeAsync(List<QuestionTypeDto> dto)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<List<QuestionType>>();
            var questionTypes = new List<QuestionType>();
            int submittedData = 0;
            try
            {
                if (!dto.Any())
                    throw new CustomException("No data supplied");

                var container = await GetContainer();
                foreach(var q in dto)
                {
                    var data = await container.Item1.CreateItemAsync(new QuestionType
                    {
                        Type = q.Type,
                        Question = q.Question
                    });
                    if(data != null)
                    {
                        submittedData++;
                        questionTypes.Add(new QuestionType
                        {
                            Type = data.Resource.Type,
                            Question = data.Resource.Question,
                        });
                    }
                }
                
                response.Status = submittedData > 0;
                response.Message = submittedData > 0 ? "Question created successfully" : "Unable to create question";
                response.Data = questionTypes;
            }
            catch (CustomException ex)
            {
                error.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
            }

            return new Tuple<ResponseHandler<List<QuestionType>>, ErrorResponse>(response, error);
        }

        public async Task<ResponseHandler<List<QuestionType>>> GetAllQuestionTypesAsync()
        {
            try
            {
                var container = await GetContainer();
                var sqlQuery = $"SELECT * FROM {container.Item2}";
                QueryDefinition queryDefinition = new(sqlQuery);
                var queryResultSetIterator = container.Item1.GetItemQueryIterator<QuestionType>(queryDefinition);
                var questions = new List<QuestionType>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var q in currentResultSet)
                        questions.Add(q);
                }

                return new ResponseHandler<List<QuestionType>>
                {
                    Status = questions.Any(),
                    Message = questions.Any() ? "Questions fetched successfully" : "No record",
                    Data = questions
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseHandler<QuestionType>> GetSingleQuestionTypeAsync(string id)
        {
            try
            {
                var container = await GetContainer();
                var question = await container.Item1.ReadItemAsync<QuestionType>(id, new PartitionKey(id));
                return new ResponseHandler<QuestionType>
                {
                    Status = question.Resource != null,
                    Message = question.Resource != null ? "Question fetched" : "No record found",
                    Data = question.Resource
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseHandler<QuestionType>> GetQuestionTypeByQuestionAsync(string question)
        {
            try
            {
                var container = await GetContainer();
                var queryable = container.Item1.GetItemLinqQueryable<QuestionType>(true);
                queryable = (IOrderedQueryable<QuestionType>)queryable.Where(item => item.Question!.ToLower() == question.ToLower());
                var _question = queryable.ToArray().FirstOrDefault();
                return new ResponseHandler<QuestionType>
                {
                    Status = _question != null,
                    Message = _question != null ? "Question fetched" : "No record found",
                    Data = _question
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseHandler<QuestionType>> GetSingleQuestionTypeAsync(QuestionCategory questionType)
        {
            try
            {
                var container = await GetContainer();
                var queryable = container.Item1.GetItemLinqQueryable<QuestionType>(true);
                queryable = (IOrderedQueryable<QuestionType>)queryable.Where(item => item.Type.Equals(questionType));
                var question = queryable.ToArray().FirstOrDefault();
                return new ResponseHandler<QuestionType>
                {
                    Status = question != null,
                    Message = question != null ? "Question fetched" : "No record found",
                    Data = question
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tuple<ResponseHandler<QuestionType>, ErrorResponse>> UpdateQuestionTypeAsync(string id, QuestionTypeDto dto)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<QuestionType>();
            try
            {
                var container = await GetContainer();
                var res = await container.Item1.ReadItemAsync<QuestionType>(id, new PartitionKey(id));

                //Get Existing Item
                var existingItem = res.Resource;
                if (existingItem is null)
                    throw new CustomException("Invalid question");

                //Replace existing item values with new values
                existingItem.Type = dto.Type;
                existingItem.Question = dto.Question;
                existingItem.ModifiedAt = DateTime.Now;
                var updateRes = await container.Item1.ReplaceItemAsync(existingItem, id, new PartitionKey(id));
                response.Status = updateRes.Resource != null;
                response.Message = updateRes.Resource != null ? "Question updated successfully" : "Unable to update record";
                response.Data = updateRes.Resource;
            }
            catch (CustomException ex)
            {
                error.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
            }

            return new Tuple<ResponseHandler<QuestionType>, ErrorResponse>(response, error);
        }
        private async Task<Tuple<Container, string>> GetContainer()
            => await databaseConnection.DatabaseConnectAsync(Table!, PartitionKey!);
    }
}
