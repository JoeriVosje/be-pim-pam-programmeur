using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;

namespace PimPamProgrammeur.API.Mapping
{
    public class ComponentMapping : Profile
    {
        public ComponentMapping()
        {
            CreateMap<Component, ComponentResponseDto>().ConvertUsing((e, _, context) => ComponentToComponentResponseDto(e, context));
            CreateMap<ComponentRequestDto, Component>().ConvertUsing((e, _, context) => ComponentRequestDtoToComponent(e));
            CreateMap<ComponentUpdateRequestDto, Component>().ConvertUsing((e, _, context) => ComponentUpdateRequestDtoToComponent(e));
        }

        

        private ComponentResponseDto ComponentToComponentResponseDto(Component component, ResolutionContext resolution)
        {
            return new ComponentResponseDto
            {
                Id = component.Id,
                Answers = AnswerToAnswerResponse(component.Answers),
                Hint = component.Hint,
                Question = component.Question,
                Skippable = component.Skippable,
                Theory = component.Theory,
                Title = component.Title,
                CorrectAnswer = component.CorrectAnswerId
            };
        }

        private Component ComponentRequestDtoToComponent(ComponentRequestDto requestDto)
        {
            return new Component
            {
                Answers = AnswerRequestDtoToAnswer(requestDto.Answers), 
                Hint = requestDto.Hint,
                ModuleId = requestDto.ModuleId,
                Question = requestDto.Question,
                Skippable = requestDto.Skippable,
                Theory = requestDto.Theory,
                Title = requestDto.Title
            };
        }

        private Component ComponentUpdateRequestDtoToComponent(ComponentUpdateRequestDto requestDto)
        {
            return new Component
            {
                Answers = AnswerUpdateRequestDtoToAnswer(requestDto.Answers),
                Hint = requestDto.Hint,
                ModuleId = requestDto.ModuleId,
                Question = requestDto.Question,
                Skippable = requestDto.Skippable,
                Theory = requestDto.Theory,
                Title = requestDto.Title,
                Id = requestDto.Id

            };

        }

        private ICollection<Answer> AnswerUpdateRequestDtoToAnswer(ICollection<AnswerUpdateRequestDto> updateRequestDto)
        {
            var listOfAnswers = new List<Answer>();

            foreach (var item in updateRequestDto)
            {
                listOfAnswers.Add(new Answer
                {
                    Description = item.Description,
                    Id = item.Id,
                    ComponentId = item.ComponentId,
                    IsCorrectAnswer = item.IsCorrectAnswer
                    
                });

            }
            return listOfAnswers;
        }

        //TODO Map this somewhere else the in the component mapping
        private ICollection<Answer> AnswerRequestDtoToAnswer(ICollection<AnswerRequestDto> e)
        {
            var listOfAnswers = new List<Answer>();

            foreach (var item in e)
            {
                listOfAnswers.Add(new Answer
                {
                    Description = item.Description,
                    IsCorrectAnswer = item.IsCorrectAnswer
                });

            }
            return listOfAnswers;
        }

        //TODO Map this somewhere else the in the component mapping
        private ICollection<AnswerResponseDto> AnswerToAnswerResponse(ICollection<Answer> e)
        {
            var listOfAnswersResponse = new List<AnswerResponseDto>();

            foreach (var item in e)
            {
                listOfAnswersResponse.Add(new AnswerResponseDto
                {
                    Id = item.Id,
                    Description = item.Description,
                    IsCorrectAnswer = item.IsCorrectAnswer
                    
                });

            }
            return listOfAnswersResponse;
        }
    }
}
