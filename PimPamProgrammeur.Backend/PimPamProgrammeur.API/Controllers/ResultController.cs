using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultProcessor _resultProcessor;
        private readonly IValidator<ResultRequestDto> _resultRequestValidator;

        public ResultController(IResultProcessor resultProcessor, IValidator<ResultRequestDto> resultRequestValidator)
        {
            _resultProcessor = resultProcessor;
            _resultRequestValidator = resultRequestValidator;
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
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ResultResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostResult(ResultRequestDto request)
        {
            var validationResult = _resultRequestValidator.Validate(request);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var result = await _resultProcessor.SaveResult(request);

            return Ok(result);
        }
    }
}
