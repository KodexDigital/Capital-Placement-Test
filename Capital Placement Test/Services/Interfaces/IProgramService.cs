using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;

namespace Capital_Placement_Test.Services.Interfaces
{
    public interface IProgramService
    {
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> CreateCandidateProgramAsync(CreateProgramDto dto);
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> UpdateCandidateProgramAsync(string id, CreateProgramDto dto);
        Task<ResponseHandler<List<CandidateProgram>>> GetAllCandidateProgramsAsync();
        Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> GetSingleCandidateProgramAsync(string id);
    }
}
