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
            var component = _repository.GetComponent(id);
            if (component == null)
            {
                return null;
            }
            var answers = _answerRepository.GetAnswersByComponentId(id);

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
            //Create the component and after that the answer has been created.
            //step 1 get the answer and check which answer has a true.
            var answers =  _answerRepository.GetAnswersByComponentId(savedComponent.Id);
            //step 2 add the corectAnswerId with the answerId.
            foreach (var item in answers)
            {
                if(item.IsCorrectAnswer == true)
                {
                    savedComponent.CorrectAnswerId = item.Id;
                }
            }

            savedComponent.Answers = answers.ToList();

            //Step 3. Update the component
            var updatedComponentWithCorrectAnswerId = await _repository.UpdateComponent(savedComponent);

            var componentResponse = _mapper.Map<ComponentResponseDto>(updatedComponentWithCorrectAnswerId);

            return componentResponse;
        }

        public async Task<ComponentResponseDto> UpdateComponent(ComponentUpdateRequestDto componentUpdateRequestDto)
        {
            var component = _mapper.Map<Component>(componentUpdateRequestDto);
            IEnumerable<Answer> foundAnswers = _answerRepository.GetAnswersByComponentId(component.Id);
            foreach (var answer in component.Answers)
            {
                Answer updateAnswer = foundAnswers.FirstOrDefault(e => e.Id == answer.Id);
                updateAnswer.Description = answer.Description;
                await _answerRepository.UpdateAnswerByComponentId(updateAnswer);

            }

            foundAnswers = _answerRepository.GetAnswersByComponentId(component.Id);
            component.Answers = foundAnswers.ToList();

            Component updatedComponent = await _repository.UpdateComponent(component);

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
