using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.DTOs.ResponseDto;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;

namespace Capital_Placement_Test.Services.Interfaces
{
    public interface IApplicationFormService
    {
        Task<ResponseHandler<ApplicationFormResponseDto>> ApplyAsync(ApplicationFormDto dto);
        Task<Tuple<ResponseHandler<ApplicationFormResponseDto>, ErrorResponse>> UpdateApplicationFormAsync(string id, ApplicationFormDto dto);
        Task<ResponseHandler<List<ApplicationForm>>> GetAllApplicationFormsAsync();
        Task<ResponseHandler<ApplicationForm>> GetSingleApplicationFormAsync(string id);
    }
}