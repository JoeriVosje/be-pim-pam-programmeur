using PimPamProgrammeur.Dto;
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
        Task<ComponentResponseDto> UpdateComponent(Guid id);

        // Get By Module Id
        ComponentResponseDto GetComponentByModuleId(Guid id);
    }
}
