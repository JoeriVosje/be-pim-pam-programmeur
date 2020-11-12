using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowedCorsPolicies")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly SessionRequestDtoValidator _sessionRequestDtoValidator;
        private readonly OpenSessionRequestDtoValidator _openSessionRequestDtoValidator;
        private readonly CloseSessionRequestDtoValidator _closeSessionRequestDtoValidator;
        private readonly ISessionProcessor _sessionProcessor;

        public SessionController(SessionRequestDtoValidator sessionRequestDtoValidator, OpenSessionRequestDtoValidator openSessionRequestDtoValidator, CloseSessionRequestDtoValidator closeSessionRequestDtoValidator, ISessionProcessor sessionProcessor)
        {
            _sessionRequestDtoValidator = sessionRequestDtoValidator;
            _openSessionRequestDtoValidator = openSessionRequestDtoValidator;
            _closeSessionRequestDtoValidator = closeSessionRequestDtoValidator;
            _sessionProcessor = sessionProcessor;
        }

        /// <summary>
        /// Opens a session
        /// </summary>
        /// <param name="sessionRequest">The session to open</param>
        /// <returns>The opened session</returns>
        [HttpPost("Open")]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(SessionRequestDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]

        public async Task<IActionResult> OpenSession(SessionRequestDto sessionRequest)
        {
            var validationResult = _openSessionRequestDtoValidator.Validate(sessionRequest);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var sessionResponseDto = await _sessionProcessor.Open(sessionRequest);

            return Ok(sessionResponseDto);
        }

        /// <summary>
        /// Closes a session
        /// </summary>
        /// <param name="sessionRequest">The session to close</param>
        /// <returns>The closed session</returns>
        [HttpPost("Close")]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(SessionRequestDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]

        public async Task<IActionResult> CloseSession(SessionRequestDto sessionRequest)
        {
            var validationResult = _closeSessionRequestDtoValidator.Validate(sessionRequest);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var sessionResponseDto = await _sessionProcessor.Close(sessionRequest);

            return Ok(sessionResponseDto);
        }

        /// <summary>
        /// Gets all sessions
        /// </summary>
        /// <returns>The sessions</returns>
        [HttpGet]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(IEnumerable<SessionResponseDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ValidationResult), 400)]

        public IActionResult GetSessions()
        {
            var sessionResponseDto = _sessionProcessor.GetSessions();
            if (!sessionResponseDto.Any())
            {
                return NoContent();
            }

            return Ok(sessionResponseDto);
        }

        /// <summary>
        /// Gets all sessions
        /// </summary>
        /// <returns>The sessions</returns>
        [HttpGet("{id}")]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(SessionResponseDto), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ValidationResult), 400)]

        public IActionResult GetSessions([FromRoute] Guid id)
        {
            var sessionResponseDto = _sessionProcessor.GetSession(id);
            if (sessionResponseDto == null)
            {
                return NoContent();
            }

            return Ok(sessionResponseDto);
        }
    }
}