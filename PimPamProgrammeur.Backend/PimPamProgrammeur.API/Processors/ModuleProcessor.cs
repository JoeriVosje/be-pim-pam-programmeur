﻿using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public class ModuleProcessor : IModuleProcessor
    {
        private readonly IMapper _mapper;
        private readonly IModuleRepository _moduleRepository;

        public ModuleProcessor(IMapper mapper, IModuleRepository moduleRepository)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
        }
        public async Task<ModuleResponseDto> SaveModule(ModuleRequestDto requestDto)
        {

            var module = _mapper.Map<Module>(requestDto);

            var resultModule = await _moduleRepository.SaveModule(module);

            return _mapper.Map<ModuleResponseDto>(resultModule);

        }
        public ModuleResponseDto GetModule(Guid id)
        {
            var module = _moduleRepository.GetModule(id);
            return module == null ? null : _mapper.Map<ModuleResponseDto>(module);
        }

        public async Task<ModuleResponseDto> UpdateModule(ModuleUpdateRequestDto requestDto)
        {
            var module = _mapper.Map<Module>(requestDto);
            
            var resultModule = await _moduleRepository.UpdateModule(module);

            return _mapper.Map<ModuleResponseDto>(resultModule);
        }

        public IEnumerable<ModuleResponseDto> GetModules()
        {
            //Connection with repository
            var modules = _moduleRepository.GetModules();
            //Return and map the module model to the ModuleResponseDTO 
            return _mapper.Map<IEnumerable<ModuleResponseDto>>(modules);
        }
    }
}
