using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Enums;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;

namespace Capital_Placement_Test.Services.Interfaces
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
