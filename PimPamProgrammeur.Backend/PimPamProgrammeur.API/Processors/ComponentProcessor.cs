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
        private readonly IAnswerRepository _answerRepository;

        public ComponentProcessor(IMapper mapper, IComponentRepository repository, IAnswerRepository answerRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _answerRepository = answerRepository;
        }

        public async Task DeleteComponent(Guid id)
        {
            await _answerRepository.DeleteAnswersByComponentId(id);
            await _repository.DeleteComponent(id);
        }

        public ComponentResponseDto GetComponent(Guid id)
        {
            //TODO WHEN ID is null set here that there's no content
            var answers = _answerRepository.GetAnswersByComponentId(id);
            var component = _repository.GetComponent(id);

            component.Answers = answers.ToList();

            return component == null ? null : _mapper.Map<ComponentResponseDto>(component);
        }

        public IEnumerable<ComponentResponseDto> GetComponents()
        {
            var components = _repository.GetComponents();
            foreach (var component in components)
            {
                var answers = _answerRepository.GetAnswersByComponentId(component.Id);
                component.Answers = answers.ToList();
            }

            return _mapper.Map<IEnumerable<ComponentResponseDto>>(components);
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
            //TODO stap 1 Get the answers from components

            //TODO Stap 2 For each answer check in Repository from answers
            // step 3 In for each update the answer description

            var updatedComponent = await _repository.UpdateComponent(component);

            return _mapper.Map<ComponentResponseDto>(updatedComponent);
        }

        public IEnumerable<ComponentResponseDto> GetComponentsByModuleId(Guid id)
        {
            var components = _repository.GetComponentsByModule(id);
            foreach (var item in components)
            {
                var answers = _answerRepository.GetAnswersByComponentId(item.Id);
                item.Answers = answers.ToList();
            }

            return _mapper.Map<IEnumerable<ComponentResponseDto>>(components);

        }

    }
}
