using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.Enums;
using cosmo_db_test.Filters;
using cosmo_db_test.Models;
using cosmo_db_test.Response;
using cosmo_db_test.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cosmo_db_test.Controllers
{
    /// <summary>
    /// Employee question setup
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiAuthenticationHeaderFilter))]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        /// <summary>
        /// Question setup constructor
        /// </summary>
        /// <param name="questionService"></param>
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Create applcation questions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ResponseHandler<List<QuestionType>>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateQuestion([FromBody] List<QuestionTypeDto> request)
        {
            var (response, error) = await questionService.CreateQuestionTypeAsync(request);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }

        /// <summary>
        /// Update question
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{questionId}")]
        [ProducesResponseType(typeof(ResponseHandler<QuestionType>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateQuestion(string questionId, [FromBody] QuestionTypeDto request)
        {
            var (response, error) = await questionService.UpdateQuestionTypeAsync(questionId, request);
            if (response.Status) return Ok(response);
            return BadRequest(error);
        }

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ResponseHandler<IReadOnlyList<QuestionType>>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetAllQuestions()
        {
            var response = await questionService.GetAllQuestionTypesAsync();
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Get single question by question id
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <returns></returns>
        [HttpGet("{questionId}")]
        [ProducesResponseType(typeof(ResponseHandler<QuestionType>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetSingleQuestionType(string questionId)
        {
            var response = await questionService.GetSingleQuestionTypeAsync(questionId);
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }
        
        /// <summary>
        /// Get question by the question type
        /// </summary>
        /// <param name="questionType">Question type</param>
        /// <returns></returns>
        [HttpGet("{questionType}")]
        [ProducesResponseType(typeof(ResponseHandler<QuestionType>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetSingleQuestionTypeByType(QuestionCategory questionType)
        {
            var response = await questionService.GetSingleQuestionTypeAsync(questionType);
            if (response.Status) return Ok(response);
            return BadRequest(response);
        }
    }
}
