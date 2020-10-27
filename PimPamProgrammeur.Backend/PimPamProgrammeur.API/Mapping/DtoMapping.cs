using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Mapping
{
    public class DtoMapping : Profile
    {
        public DtoMapping()
        {
            CreateMap<ModuleRequestDto, Module>().ConvertUsing(e => ModuleRequestDtoToModule(e));
            CreateMap<Module, ModuleResponseDto>().ConvertUsing(e => ModuleToModuleResponseDto(e));
        }

        private ModuleResponseDto ModuleToModuleResponseDto(Module module)
        {
            return new ModuleResponseDto()
            {
                Id = module.Id,
                Name = module.Name
            };
        }

        private Module ModuleRequestDtoToModule(ModuleRequestDto moduleDto)
        {
            return new Module()
            {
                Id = Guid.NewGuid(),
                Name = moduleDto.Name
            };
        }
    }
}
