using PimPamProgrammeur.Dto;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IModuleProcessor
    {
        Task<ModuleResponseDto> SaveModule(ModuleRequestDto requestDto);
    }
}