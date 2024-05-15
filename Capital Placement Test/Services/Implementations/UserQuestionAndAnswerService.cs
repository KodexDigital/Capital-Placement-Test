using Capital_Placement_Test.Exceptions;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Capital_Placement_Test.Services.Implementations
{
    public class UserQuestionAndAnswerService : IUserQuestionAndAnswerService
    {
        private readonly IDatabaseConnection databaseConnection;
        /// <summary>
        /// Database container name alia Table
        /// </summary>
        protected internal string? Table { get; } = nameof(UserQuestionAndAnswer);

        /// <summary>
        /// Unique prorty for getting container items.
        /// This is should not be changes by any means
        /// </summary>
        protected internal string? PartitionKey { get; set; } = "clientKey";
        public UserQuestionAndAnswerService(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public async Task<ResponseHandler<UserQuestionAndAnswer>> SubmitQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer)
        {
            try
            {
                var container = await GetContainer();
                var data = await container.Item1.CreateItemAsync(questionAndAnswer);
                return new ResponseHandler<UserQuestionAndAnswer>
                {
                    Status = data != null,
                    Message = data != null ? "Question and answer submitted successfully" : "Unable to submit question and answer",
                    Data = data!.Resource
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tuple<ResponseHandler<UserQuestionAndAnswer>, ErrorResponse>> UpdateQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<UserQuestionAndAnswer>();
            try
            {
                var container = await GetContainer();
                var res = await container.Item1.ReadItemAsync<UserQuestionAndAnswer>(questionAndAnswer.Id, new PartitionKey(questionAndAnswer.Id));

                //Get Existing Item
                var existingItem = res.Resource;
                if (existingItem is null)
                    throw new CustomException("Invalid question");

                //Replace existing item values with new values
                existingItem.QuestionTypeId = questionAndAnswer.QuestionTypeId;
                existingItem.ApplicationFormId = questionAndAnswer.ApplicationFormId;
                existingItem.ModifiedAt = DateTime.Now;
                var updateRes = await container.Item1.ReplaceItemAsync(existingItem, questionAndAnswer.QuestionTypeId, new PartitionKey(questionAndAnswer.Id));
                response.Status = updateRes.Resource != null;
                response.Message = updateRes.Resource != null ? "Question answer updated successfully" : "Unable to update record";
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

            return new Tuple<ResponseHandler<UserQuestionAndAnswer>, ErrorResponse>(response, error);
        }
        private async Task<Tuple<Container, string>> GetContainer()
            => await databaseConnection.DatabaseConnectAsync(Table!, PartitionKey!);
    }
}
