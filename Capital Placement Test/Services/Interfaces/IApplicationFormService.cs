using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.DTOs.ResponseDto;
using cosmo_db_test.Models;
using cosmo_db_test.Response;

namespace cosmo_db_test.Services.Interfaces
{
    public interface IApplicationFormService
    {
        Task<ResponseHandler<ApplicationFormResponseDto>> ApplyAsync(ApplicationFormDto dto);
        Task<Tuple<ResponseHandler<ApplicationFormResponseDto>, ErrorResponse>> UpdateApplicationFormAsync(string id, ApplicationFormDto dto);
        Task<ResponseHandler<List<ApplicationForm>>> GetAllApplicationFormsAsync();
        Task<ResponseHandler<ApplicationForm>> GetSingleApplicationFormAsync(string id);
    }
}