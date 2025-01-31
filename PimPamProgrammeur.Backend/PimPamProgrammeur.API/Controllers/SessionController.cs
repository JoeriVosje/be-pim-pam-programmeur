﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using PimPamProgrammeur.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace PimPamProgrammeur.API.Controllers
{
    [EnableCors(Constants.AllowedCorsPolicies)]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly SessionRequestDtoValidator _sessionRequestDtoValidator;
        private readonly OpenSessionRequestDtoValidator _openSessionRequestDtoValidator;
        private readonly CloseSessionRequestDtoValidator _closeSessionRequestDtoValidator;
        private readonly ISessionProcessor _sessionProcessor;
        private readonly ITokenProvider _tokenProvider;

        public SessionController(SessionRequestDtoValidator sessionRequestDtoValidator, OpenSessionRequestDtoValidator openSessionRequestDtoValidator, CloseSessionRequestDtoValidator closeSessionRequestDtoValidator, ISessionProcessor sessionProcessor, ITokenProvider tokenProvider)
        {
            _sessionRequestDtoValidator = sessionRequestDtoValidator;
            _openSessionRequestDtoValidator = openSessionRequestDtoValidator;
            _closeSessionRequestDtoValidator = closeSessionRequestDtoValidator;
            _sessionProcessor = sessionProcessor;
            _tokenProvider = tokenProvider;
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
        [AuthorizeStudent]
        [ProducesResponseType(typeof(IEnumerable<SessionResponseDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ValidationResult), 400)]

        public IActionResult GetSessions([FromQuery] Guid? moduleId)
        {
            var sessionResponseDto = _sessionProcessor.GetSessions(moduleId);
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

        public IActionResult GetSession([FromRoute] Guid id)
        {
            var sessionResponseDto = _sessionProcessor.GetSession(id);
            if (sessionResponseDto == null)
            {
                return NoContent();
            }

            return Ok(sessionResponseDto);
        }

        /// <summary>
        /// Gets current status
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        [ProducesResponseType(typeof(SessionStatusResponseDto), 200)]
        [AuthorizeStudent]
        public IActionResult GetSession()
        {
            var token = Request.Headers[HeaderNames.Authorization];
            var userId = _tokenProvider.GetUserId(token);
            if (!userId.HasValue)
            {
                return BadRequest();
            }

            return FindSessionInfoByUserId(userId.Value);
        }

        /// <summary>
        /// Gets current status
        /// </summary>
        /// <returns></returns>
        [HttpGet("status/{userId}")]
        [ProducesResponseType(typeof(SessionStatusResponseDto), 200)]
        [AuthorizeAdmin]
        public IActionResult GetSessionInfo(Guid userId)
        {
            return FindSessionInfoByUserId(userId);
        }

        private IActionResult FindSessionInfoByUserId(Guid userId)
        {
            var sessionStatusResponseDto = _sessionProcessor.GetSessionStatus(userId);
            return Ok(sessionStatusResponseDto);
        }
    }
}