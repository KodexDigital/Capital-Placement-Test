using cosmo_db_test.Models;
using cosmo_db_test.Response;

namespace cosmo_db_test.Services.Interfaces
{
    public interface IUserQuestionAndAnswerService
    {
        Task<ResponseHandler<UserQuestionAndAnswer>> SubmitQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer);
        Task<Tuple<ResponseHandler<UserQuestionAndAnswer>, ErrorResponse>> UpdateQuestionAnswerAsync(UserQuestionAndAnswer questionAndAnswer);
    }
}
