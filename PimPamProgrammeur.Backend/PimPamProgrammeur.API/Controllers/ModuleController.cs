using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Dto;

namespace PimPamProgrammeur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ModuleController : ControllerBase
    {
        private readonly IModuleProcessor _processor;
        public ModuleController(IModuleProcessor processor)
        {
            _processor = processor;
        }
        [HttpPost]
        public async Task<IActionResult> PostModule(ModuleRequestDto request)
        {
            var module = await _processor.SaveModule(request);

            return Ok(module);
        }
    }
}
