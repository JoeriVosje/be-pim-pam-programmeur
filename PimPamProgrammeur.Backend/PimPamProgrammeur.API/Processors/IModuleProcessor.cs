using PimPamProgrammeur.Dto;
using System;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IModuleProcessor
    {
        Task<ModuleResponseDto> SaveModule(ModuleRequestDto requestDto);
        ModuleResponseDto GetModule(Guid id);
    }
}