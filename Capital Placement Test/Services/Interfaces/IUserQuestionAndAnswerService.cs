using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;

namespace Capital_Placement_Test.Services.Interfaces
{
    public interface IUserQuestionAndAnswerService
    {
        Task<ResponseHandler<UserQuestionAndAnswer>> SubmitQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer);
        Task<Tuple<ResponseHandler<UserQuestionAndAnswer>, ErrorResponse>> UpdateQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer);
    }
}
