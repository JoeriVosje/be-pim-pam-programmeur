using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using PimPamProgrammeur.Utils;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace PimPamProgrammeur.API.Controllers
{
    [EnableCors(Constants.AllowedCorsPolicies)]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultProcessor _resultProcessor;
        private readonly ITokenProvider _tokenProvider;
        private readonly IValidator<ResultRequestDto> _resultRequestValidator;
        private readonly IValidator<EmptyResultRequestDto> _emptyResultRequestValidator;

        public ResultController(IResultProcessor resultProcessor, ITokenProvider tokenProvider, IValidator<ResultRequestDto> resultRequestValidator, IValidator<EmptyResultRequestDto> emptyResultRequestValidator)
        {
            _resultProcessor = resultProcessor;
            _tokenProvider = tokenProvider;
            _resultRequestValidator = resultRequestValidator;
            _emptyResultRequestValidator = emptyResultRequestValidator;
        }

        /* /// <summary>
         /// Finds a result by sessionId and userId
         /// </summary>
         /// <param name="sessionId"></param>
         /// <param name="userId"></param>
         /// <returns>The result</returns>
         [HttpGet("{sessionId}/{userId}")]
         [ProducesResponseType(typeof(ResultResponseDto), 200)]
         [ProducesResponseType(204)]
         public IActionResult FindResult([FromRoute] Guid sessionId, Guid userId)
         {
             var result = _resultProcessor.FindResults(sessionId, userId);
             if (result == null)
             {
                 return NoContent();
             }

             return Ok(result);
         }*/

        /// <summary>
        /// Saves a result to the database
        /// </summary>
        /// <param name="request">The result to save</param>
        /// <returns>The saved result</returns>
        [HttpPost]
        [AuthorizeStudent]
        [ProducesResponseType(typeof(ResultResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostResult(ResultRequestDto request)
        {
            if (request.UserId == null && !ReadUserIdFromHeader(request))
            {
                return null;
            }

            var validationResult = _resultRequestValidator.Validate(request);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var result = await _resultProcessor.SaveResult(request);

            return Ok(result);
        }

        [HttpPost("skip")]
        [AuthorizeStudent]
        [ProducesResponseType(typeof(ResultResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> SkipAnswer(EmptyResultRequestDto request)
        {
            if (request.UserId == null && !ReadUserIdFromHeaderForEmptyResult(request))
            {
                return null;
            }

            var validationResult = _emptyResultRequestValidator.Validate(request);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var result = await _resultProcessor.SaveEmptyResult(request);

            return Ok(result);

        }

        [HttpGet]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ResultInfoResponseDto), 200)]
        public IActionResult GetAllResults(Guid? sessionId)
        {
            return Ok(_resultProcessor.GetResults(sessionId));
        }

        private bool ReadUserIdFromHeader(ResultRequestDto request)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            var userId = _tokenProvider.GetUserId(token);
            if (!userId.HasValue)
            {
                return false;
            }

            request.UserId = userId;
            return true;
        }

        private bool ReadUserIdFromHeaderForEmptyResult(EmptyResultRequestDto request)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            var userId = _tokenProvider.GetUserId(token);
            if (!userId.HasValue)
            {
                return false;
            }

            request.UserId = userId;
            return true;
        }

    }
}
