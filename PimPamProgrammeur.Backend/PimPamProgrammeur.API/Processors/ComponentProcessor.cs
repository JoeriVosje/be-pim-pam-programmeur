using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public class ComponentProcessor : IComponentProcessor
    {
        private readonly IMapper _mapper;
        private readonly IComponentRepository _repository;

        public ComponentProcessor(IMapper mapper, IComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task DeleteComponent(Guid id)
        {
            await _repository.DeleteComponent(id);
        }

        public ComponentResponseDto GetComponent(Guid id)
        {
            var component = _repository.GetComponent(id);

            return component == null ? null : _mapper.Map<ComponentResponseDto>(component);
        }

        public IEnumerable<Component> GetComponents()
        {
            var components = _repository.GetComponents();

            return components;
        }

        public async Task<ComponentResponseDto> SaveComponent(ComponentRequestDto componentRequest)
        {
            var component = _mapper.Map<Component>(componentRequest);
            var savedComponent = await _repository.SaveComponent(component);
            var componentResponse = _mapper.Map<ComponentResponseDto>(savedComponent);

            return componentResponse;
        }

        public async Task<ComponentResponseDto> UpdateComponent(Guid id)
        {
            var component = _mapper.Map<Component>(id);
            var updatedComponent = await _repository.UpdateComponent(component);

            return _mapper.Map<ComponentResponseDto>(updatedComponent);
        }
    }
}
