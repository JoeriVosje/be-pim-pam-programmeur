﻿using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IComponentProcessor
    {
        // Put
        Task<ComponentResponseDto> SaveComponent(ComponentRequestDto componentRequest);

        // Get
        ComponentResponseDto GetComponent(Guid id);

        // Get all
        IEnumerable<ComponentResponseDto> GetComponents();

        // Delete
        Task DeleteComponent(Guid id);

        // Post 
        Task<ComponentResponseDto> UpdateComponent(ComponentUpdateRequestDto componentUpdateRequestDto);

        // Get By Module Id
        IEnumerable<ComponentResponseDto> GetComponentsByModuleId(Guid id);
        Task<IEnumerable<ComponentResponseDto>> OrderComponents(ComponentOrderRequestDto componentOrderRequestDto);
    }
}
