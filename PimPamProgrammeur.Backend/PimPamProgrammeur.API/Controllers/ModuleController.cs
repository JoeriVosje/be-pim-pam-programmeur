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
        private readonly IModuleProcessor _processor;
        private readonly IValidator<ModuleRequestDto> _moduleRequestValidator;

        public ModuleController(IModuleProcessor processor, IValidator<ModuleRequestDto> moduleRequestValidator)
        {
            _processor = processor;
            _moduleRequestValidator = moduleRequestValidator;
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

            var module = await _processor.SaveModule(request);

            return Ok(module);
        }
    }
}
