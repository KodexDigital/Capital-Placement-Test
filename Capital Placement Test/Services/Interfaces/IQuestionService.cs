using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.Enums;
using cosmo_db_test.Models;
using cosmo_db_test.Response;

namespace cosmo_db_test.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Tuple<ResponseHandler<List<QuestionType>>, ErrorResponse>> CreateQuestionTypeAsync(List<QuestionTypeDto> dto);
        Task<Tuple<ResponseHandler<QuestionType>, ErrorResponse>> UpdateQuestionTypeAsync(string id, QuestionTypeDto dto);
        Task<ResponseHandler<List<QuestionType>>> GetAllQuestionTypesAsync();
        Task<ResponseHandler<QuestionType>> GetSingleQuestionTypeAsync(string id);
        Task<ResponseHandler<QuestionType>> GetQuestionTypeByQuestionAsync(string question);
        Task<ResponseHandler<QuestionType>> GetSingleQuestionTypeAsync(QuestionCategory questionType);
    }
}
