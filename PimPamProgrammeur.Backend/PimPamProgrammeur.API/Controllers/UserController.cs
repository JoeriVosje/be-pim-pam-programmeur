using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;

namespace PimPamProgrammeur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProcessor _userProcessor;
        private readonly IValidator<UserRequestDto> _userRequestValidator;
        private readonly IValidator<UserLoginRequestDto> _userLoginRequestValidator;

        public UserController(IUserProcessor userProcessor, IValidator<UserRequestDto> userRequestValidator, IValidator<UserLoginRequestDto> userLoginRequestValidator)
        {
            _userProcessor = userProcessor;
            _userRequestValidator = userRequestValidator;
            _userLoginRequestValidator = userLoginRequestValidator;
        }

        /// <summary>
        /// Saves a user to the database
        /// </summary>
        /// <param name="userRequestDto">The user to save</param>
        /// <returns>The saved module</returns>
        [HttpPost]
        //[AuthorizeAdmin]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostUser(UserRequestDto userRequestDto)
        {
            var validationResult = _userRequestValidator.Validate(userRequestDto);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var userResponseDto = await _userProcessor.SaveUser(userRequestDto);

            return Ok(userResponseDto);
        }

        /// <summary>
        /// Logs a user into the system
        /// </summary>
        /// <param name="userLoginRequestDto">The user to login with</param>
        /// <returns>The saved module</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public IActionResult Login(UserLoginRequestDto userLoginRequestDto)
        {
            var validationResult = _userLoginRequestValidator.Validate(userLoginRequestDto);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var accessToken = _userProcessor.Login(userLoginRequestDto);
            if (accessToken == null)
            {
                return Unauthorized();
            }

            return Ok(accessToken);
        }

        /// <summary>
        /// Gets a single user by Id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The saved module</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        [ProducesResponseType(204)]
        [AuthorizeAdmin] // TODO Do we need to allow this endpoint for students?
        public IActionResult GetUser([FromRoute] Guid id)
        {
            var user = _userProcessor.GetUser(id);
            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>The saved module</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), 200)]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public IActionResult GetUser()
        {
            var users = _userProcessor.GetUsers().ToList();
            if (users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        /// <summary>
        /// Gets a single user by Id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The saved module</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _userProcessor.DeleteUser(id);

            return Ok();
        }


    }
}
