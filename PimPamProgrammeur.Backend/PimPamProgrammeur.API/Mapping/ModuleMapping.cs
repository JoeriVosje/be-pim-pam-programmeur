using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;

namespace PimPamProgrammeur.API.Mapping
{
    public class ModuleMapping : Profile
    {
        public ModuleMapping()
        {
            CreateMap<ModuleRequestDto, Module>().ConvertUsing(e => ModuleRequestDtoToModule(e));
            CreateMap<Module, ModuleResponseDto>().ConvertUsing(e => ModuleToModuleResponseDto(e));
        }

        private ModuleResponseDto ModuleToModuleResponseDto(Module module)
        {
            return new ModuleResponseDto()
            {
                Id = module.Id,
                Name = module.Name,
                CreationDate = module.CreationDate
            };
        }

        private Module ModuleRequestDtoToModule(ModuleRequestDto moduleDto)
        {
            return new Module()
            {
                Name = moduleDto.Name
            };
        }
    }
}
