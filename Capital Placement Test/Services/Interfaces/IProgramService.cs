using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.Models;
using cosmo_db_test.Response;

namespace cosmo_db_test.Services.Interfaces
{
    public interface IProgramService
    {
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> CreateCandidateProgramAsync(CreateProgramDto dto);
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> UpdateCandidateProgramAsync(string id, CreateProgramDto dto);
        Task<ResponseHandler<List<CandidateProgram>>> GetAllCandidateProgramsAsync();
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> GetSingleCandidateProgramAsync(string id);
    }
}
