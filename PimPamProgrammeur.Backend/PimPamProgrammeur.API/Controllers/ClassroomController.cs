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
    [EnableCors("AllowedCorsPolicies")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomProcessor _classroomProcessor;
        private readonly IValidator<ClassroomRequestDto> _classroomRequestValidator;

        public ClassroomController(IClassroomProcessor classroomProcessor, IValidator<ClassroomRequestDto> classroomRequestValidator)
        {
            _classroomProcessor = classroomProcessor;
            _classroomRequestValidator = classroomRequestValidator;
        }

        /// <summary>
        /// Saves a classroom to the database.
        /// </summary>
        /// <param name="classroomRequest">The classroom that needs to be saved.</param>
        /// <returns>The saved classroom.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ClassroomResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostClassroom(ClassroomRequestDto classroomRequest)
        {
            var validationResult = _classroomRequestValidator.Validate(classroomRequest);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var classroomResponseDto = await _classroomProcessor.SaveClassroom(classroomRequest);

            return Ok(classroomResponseDto);
        }

        /// <summary>
        /// Gets a classroom from the database.
        /// </summary>
        /// <param name="id">The key of the classroom.</param>
        /// <returns>The classroom if found, else nothing.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClassroomResponseDto), 200)]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public IActionResult GetClassroom([FromRoute] Guid id)
        {
            var classroom = _classroomProcessor.GetClassroom(id);
            if (classroom == null)
            {
                return NoContent();
            }

            return Ok(classroom);
        }

        /// <summary>
        /// Deletes a classroom by a given key.
        /// </summary>
        /// <param name="id">The key of the classroom that needs to be deleted.</param>
        /// <returns>Nothing.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public async Task<IActionResult> DeleteClassroom([FromRoute] Guid id)
        {
            await _classroomProcessor.DeleteClassroom(id);

            return Ok();
        }

        /// <summary>
        /// Get all the classrooms
        /// </summary>
        /// <returns>A list of classrooms</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassroomResponseDto>), 200)]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public IActionResult GetAllClassrooms()
        {
            var classrooms = _classroomProcessor.GetAllClassrooms().ToList();
            if (classrooms.Count == 0)
            {
                return NoContent();
            }

            return Ok(classrooms);
        }
    }
}
