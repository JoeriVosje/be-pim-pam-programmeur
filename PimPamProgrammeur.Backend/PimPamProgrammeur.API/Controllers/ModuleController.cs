using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Auth;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;


namespace PimPamProgrammeur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ModuleController : ControllerBase
    {
        private readonly IModuleProcessor _moduleProcessor;
        private readonly IValidator<ModuleRequestDto> _moduleRequestValidator;
        private readonly IValidator<ModuleUpdateRequestDto> _moduleUpdateRequestValidator;


        public ModuleController(IModuleProcessor processor, IValidator<ModuleRequestDto> moduleRequestValidator, IValidator<ModuleUpdateRequestDto> moduleUpdateRequestValidator)
        {
            _moduleProcessor = processor;
            _moduleRequestValidator = moduleRequestValidator;
            _moduleUpdateRequestValidator = moduleUpdateRequestValidator;
        }

        /// <summary>
        /// Gets a module from the database
        /// </summary>
        /// <param name="request">The module to get</param>
        /// <returns>The module</returns>
        [HttpGet("{id}")]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ModuleResponseDto), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public IActionResult GetModule([FromRoute] Guid id)
        {
            var module = _moduleProcessor.GetModule(id);
            if (module == null)
            {
                return NoContent();
            }

            return Ok(module);
        }

        /// <summary>
        /// Saves a module to the database
        /// </summary>
        /// <param name="request">The module to save</param>
        /// <returns>The saved module</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ModuleResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostModule(ModuleRequestDto request)
        {
            var validationResult = _moduleRequestValidator.Validate(request);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var module = await _moduleProcessor.SaveModule(request);

            return Ok(module);
        }

        /// <summary>
        /// Change name from Module
        /// </summary>
        /// <returns>The updated module</returns>
        [HttpPut]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ModuleResponseDto), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PutModule(ModuleUpdateRequestDto request)
        {
            var validationResult = _moduleUpdateRequestValidator.Validate(request);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var module = await _moduleProcessor.UpdateModule(request);

            return Ok(module);

        }
    }
}
