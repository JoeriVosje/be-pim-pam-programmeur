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
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentProcessor _componentProcessor;
        private readonly IValidator<ComponentRequestDto> _validator;
        private readonly IValidator<ComponentUpdateRequestDto> _requestValidator;

        public ComponentController(IComponentProcessor componentProcessor, IValidator<ComponentRequestDto> validator, IValidator<ComponentUpdateRequestDto> requestValidator)
        {
            _componentProcessor = componentProcessor;
            _validator = validator;
            _requestValidator = requestValidator;
        }

        [HttpPost]
        [AuthorizeAdmin]
        [ProducesResponseType(typeof(ComponentResponseDto), 200)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostComponent(ComponentRequestDto componentRequest)
        {
            var validationResult = _validator.Validate(componentRequest);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult);
            }

            var componentResponseDto = await _componentProcessor.SaveComponent(componentRequest);

            return Ok(componentResponseDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComponentResponseDto), 200)]
        [ProducesResponseType(204)]
        [AuthorizeStudent]
        public IActionResult GetComponent([FromRoute] Guid id)
        {
            var component = _componentProcessor.GetComponent(id);
            if (component == null)
            {
                return NoContent();
            }

            return Ok(component);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public async Task<IActionResult> DeleteComponent([FromRoute] Guid id)
        {
            await _componentProcessor.DeleteComponent(id);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ComponentResponseDto>), 200)]
        [ProducesResponseType(204)]
        [AuthorizeAdmin]
        public ActionResult GetComponents()
        {
            var components = _componentProcessor.GetComponents().ToList();
            if (components.Count == 0)
            {
                return NoContent();
            }

            return Ok(components);
        }

        //[HttpPut]
        //[AuthorizeAdmin]
        //[ProducesResponseType(typeof(ComponentResponseDto), 200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(typeof(ValidationResult), 400)]
        //public async Task<IActionResult> UpdateComponent(ComponentUpdateRequestDto componentRequest)
        //{
        //    var validationResult = _requestValidator.Validate(componentRequest);
        //    if (validationResult.Errors.Any())
        //    {
        //        return BadRequest(validationResult);
        //    }

        //    var component = await _componentProcessor.UpdateComponent(componentRequest);

        //    return Ok(component);
        //}

        [HttpGet("module/{moduleid}")]
        [AuthorizeStudent]
        [ProducesResponseType(typeof(ComponentResponseDto), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetComponentsByModuleId(Guid moduleid)
        {
            var component = _componentProcessor.GetComponentsByModuleId(moduleid);
            if (component == null)
            {
                return NoContent();
            }

            return Ok(component);
        }
    }
}
