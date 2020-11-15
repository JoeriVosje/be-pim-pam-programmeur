using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IModuleProcessor
    {
        Task<ModuleResponseDto> SaveModule(ModuleRequestDto requestDto);
        ModuleResponseDto GetModule(Guid id);
        Task<ModuleResponseDto> UpdateModule(ModuleUpdateRequestDto requestDto);
        IEnumerable<ModuleResponseDto> GetModules();
        Task<ValidationResult> DeleteModule(Guid id);


    }
}