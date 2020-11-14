using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Mapping
{
    public class AnswerMapping : Profile
    {
        public AnswerMapping()
        {
            CreateMap<Answer, AnswerResponseDto>().ConvertUsing((e, _, context) => AnswerToAnswerResponseDto(e, context));
        }

        private AnswerResponseDto AnswerToAnswerResponseDto(Answer answerDto, object context)
        {
            return new AnswerResponseDto
            {
                Id = answerDto.Id,
                Description = answerDto.Description
            };
        }
    }
}
