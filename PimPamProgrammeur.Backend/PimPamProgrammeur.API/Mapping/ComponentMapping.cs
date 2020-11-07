using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Mapping
{
    public class ComponentMapping : Profile
    {
        public ComponentMapping()
        {
            CreateMap<Component, ComponentResponseDto>().ConvertUsing((e, _, context) => ComponentToComponentResponseDto(e, context));
            CreateMap<ComponentRequestDto, Component>().ConvertUsing((e, _, context) => ComponentRequestDtoToComponent(e));
        }

        private ComponentResponseDto ComponentToComponentResponseDto(Component component, ResolutionContext resolution)
        {
            return new ComponentResponseDto
            {
                Id = component.Id,
                Answers = component.Answers,
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
                Answers = requestDto.Answers,
                Hint = requestDto.Hint,
                ModuleId = requestDto.ModuleId,
                Question = requestDto.Question,
                Skippable = requestDto.Skippable,
                Theory = requestDto.Theory,
                Title = requestDto.Title
            };
        }
    }
}
