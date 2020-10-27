﻿using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
