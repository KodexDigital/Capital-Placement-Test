using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.DTOs.ResponseDto;
using Capital_Placement_Test.Filters;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capital_Placement_Test.Controllers
{
    /// <summary>
    /// Candidate application controleer
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiAuthenticationHeaderFilter))]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationFormService applicationService;

        /// <summary>
        /// Application contructor
        /// </summary>
        /// <param name="applicationService"></param>
        public ApplicationsController(IApplicationFormService applicationService)
        {
            this.applicationService = applicationService;
        }

        /// <summary>
        /// Candidate apply for program
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("apply")]
        [ProducesResponseType(typeof(ResponseHandler<ApplicationFormResponseDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Apply([FromBody] ApplicationFormDto request)
        {
            var response = await applicationService.ApplyAsync(request);
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Update application
        /// </summary>
        /// <param name="applicationId">Application id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{applicationId}")]
        [ProducesResponseType(typeof(ResponseHandler<ApplicationFormResponseDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateApplication(string applicationId, [FromBody] ApplicationFormDto request)
        {
            var (response, error) = await applicationService.UpdateApplicationFormAsync(applicationId, request);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }

        /// <summary>
        /// Get all applications
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ResponseHandler<IReadOnlyList<ApplicationForm>>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetAllApplications()
        {
            var response = await applicationService.GetAllApplicationFormsAsync();
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Get single application
        /// </summary>
        /// <param name="applicationId">Application id</param>
        /// <returns></returns>
        [HttpGet("{applicationId}")]
        [ProducesResponseType(typeof(ResponseHandler<ApplicationForm>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetSingleCandidateApplication(string applicationId)
        {
            var response = await applicationService.GetSingleApplicationFormAsync(applicationId);
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }
    }
}
