using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.DTOs.ResponseDto;
using cosmo_db_test.Exceptions;
using cosmo_db_test.Models;
using cosmo_db_test.Response;
using cosmo_db_test.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace cosmo_db_test.Services.Implementations
{
    public class ApplicationFormService : IApplicationFormService
    {
        private readonly IQuestionService questionService;
        private readonly IUserQuestionAndAnswerService questionAndAnswerService;
        private readonly IDatabaseConnection databaseConnection;
        /// <summary>
        /// Database container name alia Table
        /// </summary>
        protected internal string? Table { get; } = nameof(ApplicationForm);

        /// <summary>
        /// Unique prorty for getting container items.
        /// This is should not be changes by any means
        /// </summary>
        protected internal string? PartitionKey { get; } = "/id";
        public ApplicationFormService(IDatabaseConnection databaseConnection, IQuestionService questionService, IUserQuestionAndAnswerService questionAndAnswerService)
        {
            this.databaseConnection = databaseConnection;
            this.questionService = questionService;
            this.questionAndAnswerService = questionAndAnswerService;
        }
        public async Task<ResponseHandler<ApplicationFormResponseDto>> ApplyAsync(ApplicationFormDto dto)
        {
            var response = new ResponseHandler<ApplicationFormResponseDto>();
            try
            {
                if (!dto.AdditionalQuestions!.Any())
                    throw new CustomException("No question with corresponding answer supplied.");

                var container = await GetContainer();
                var application = new ApplicationForm
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    Nationality = dto.Nationality,
                    CurrentResidence = dto.CurrentResidence,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    IdentityNumber = dto.IdentityNumber
                };
                var data = await container.Item1.CreateItemAsync(application, new PartitionKey(PartitionKey));
                if(data != null)
                {
                    foreach (var q in dto.AdditionalQuestions!)
                    {
                        var question = await questionService.GetQuestionTypeByQuestionAsync(q.Question!.ToLower());
                        if(question is null)
                            throw new CustomException($"Selected queston: [{q.Question}] does not exist");

                        //save data
                        await questionAndAnswerService.SubmitQuestionAnswerAsync(new UserQuestionAndAnswer
                        {
                            QuestionTypeId = question.Data!.Id,
                            ApplicationFormId = data!.Resource.Id
                        });
                    }

                }

                response.Status = data != null;
                response.Message = data != null ? "Application submitted successfully" : "Unable to submit application";
                response.Data = data != null ? GetApplicationResponse(data) : null;

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseHandler<List<ApplicationForm>>> GetAllApplicationFormsAsync()
        {
            try
            {
                var container = await GetContainer();
                var sqlQuery = $"SELECT * FROM {container.Item2}";
                QueryDefinition queryDefinition = new(sqlQuery);
                var queryResultSetIterator = container.Item1.GetItemQueryIterator<ApplicationForm>(queryDefinition);
                var applications = new List<ApplicationForm>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var a in currentResultSet)
                        applications.Add(a);
                }

                return new ResponseHandler<List<ApplicationForm>>
                {
                    Status = applications.Any(),
                    Message = applications.Any() ? "Applications fetched successfully" : "No record",
                    Data = applications
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseHandler<ApplicationForm>> GetSingleApplicationFormAsync(string id)
        {
            var response = new ResponseHandler<ApplicationForm>();
            try
            {
                var container = await GetContainer();
                var application = await container.Item1.ReadItemAsync<ApplicationForm>(id, new PartitionKey(PartitionKey));
                response.Status = application != null;
                response.Message = application != null ? "Application fetched" : "No record found";
                response.Data = application!.Resource;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Tuple<ResponseHandler<ApplicationFormResponseDto>, ErrorResponse>> UpdateApplicationFormAsync(string id, ApplicationFormDto dto)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<ApplicationFormResponseDto>();
            try
            {
                var container = await GetContainer();
                var res = await container.Item1.ReadItemAsync<ApplicationForm>(id, new PartitionKey(PartitionKey));

                //Get Existing Item
                var existingItem = res.Resource;
                if (existingItem is null)
                    throw new CustomException("Invalid application");

                //Replace existing item values with new values
                existingItem.FirstName = dto.FirstName;
                existingItem.LastName = dto.LastName;
                existingItem.DateOfBirth = dto.DateOfBirth;
                existingItem.Nationality = dto.Nationality;
                existingItem.IdentityNumber = dto.IdentityNumber;
                existingItem.PhoneNumber = dto.PhoneNumber;
                existingItem.Email = dto.Email;
                existingItem.Gender = dto.Gender;
                existingItem.IdentityNumber = dto.IdentityNumber;
                existingItem.ModifiedAt = DateTime.Now;
                var updateRes = await container.Item1.ReplaceItemAsync(existingItem, id, new PartitionKey(PartitionKey));
                if (updateRes != null)
                {
                    foreach (var q in dto.AdditionalQuestions!)
                    {
                        var question = await questionService.GetQuestionTypeByQuestionAsync(q.Question!.ToLower());
                        if (question is null)
                            throw new CustomException($"Selected queston: [{q.Question}] does not exist");

                        var additionalQuestion = new UserQuestionAndAnswer
                        {
                            QuestionTypeId = question.Data!.Id,
                            ApplicationFormId = updateRes.Resource.Id
                        };

                        //update data
                        await questionAndAnswerService.UpdateQuestionAnswerAsync(new UserQuestionAndAnswer
                        {
                            QuestionTypeId = question.Data!.Id,
                            ApplicationFormId = updateRes!.Resource.Id
                        });

                    }
                }

                response.Status = updateRes != null;
                response.Message = updateRes != null ? "Application updated successfully" : "Unable to update record";
                response.Data = updateRes != null ? GetApplicationResponse(updateRes) : null;
            }
            catch (CustomException ex)
            {

                error.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
                throw;
            }

            return new Tuple<ResponseHandler<ApplicationFormResponseDto>, ErrorResponse>(response, error);
        }

        private static ApplicationFormResponseDto GetApplicationResponse(ItemResponse<ApplicationForm> responseData)
        {
            return new ApplicationFormResponseDto
            {
                FirstName = responseData.Resource.FirstName,
                LastName = responseData.Resource.LastName,
                PhoneNumber = responseData.Resource.PhoneNumber,
                Email = responseData.Resource.Email,
                Nationality = responseData.Resource.Nationality,
                CurrentResidence = responseData.Resource.CurrentResidence,
                Gender = responseData.Resource.Gender,
                DateOfBirth = responseData.Resource.DateOfBirth,
                IdentityNumber = responseData.Resource.IdentityNumber,
            };
        }

        private async Task<Tuple<Container, string>> GetContainer()
            => await databaseConnection.DatabaseConnectAsync(Table!, PartitionKey!);
    }
}