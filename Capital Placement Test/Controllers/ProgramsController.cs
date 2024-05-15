using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Filters;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capital_Placement_Test.Controllers
{
    /// <summary>
    /// Porgram setup controller by employee
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiAuthenticationHeaderFilter))]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService programApplication;

        /// <summary>
        /// Program constructor
        /// </summary>
        /// <param name="programApplication"></param>
        public ProgramsController(IProgramService programApplication)
        {
            this.programApplication = programApplication;
        }

        /// <summary>
        /// Create program for canditate application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ResponseHandler<CandidateProgram>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto request)
        {
            var (response, error) = await programApplication.CreateCandidateProgramAsync(request);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }
        
        /// <summary>
        /// Update a program
        /// </summary>
        /// <param name="programId">Program id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{programId}")]
        [ProducesResponseType(typeof(ResponseHandler<CandidateProgram>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateProgram(string programId, [FromBody] CreateProgramDto request)
        {
            var (response, error) = await programApplication.UpdateCandidateProgramAsync(programId, request);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }

        /// <summary>
        /// Get all programs
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ResponseHandler<IReadOnlyList<CandidateProgram>>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetAllPrograms()
        {
            var response = await programApplication.GetAllCandidateProgramsAsync();
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Get a single program
        /// </summary>
        /// <param name="programId">Program id</param>
        /// <returns></returns>
        [HttpGet("{programId}")]
        [ProducesResponseType(typeof(ResponseHandler<IReadOnlyList<CandidateProgram>>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetSingleCandidateProgram(string programId)
        {
            var (response, error) = await programApplication.GetSingleCandidateProgramAsync(programId);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }
    }
}