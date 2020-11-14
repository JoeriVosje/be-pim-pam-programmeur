using System;
using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.API.Mapping
{
    public class ResultMapping : Profile
    {
        public ResultMapping()
        {
            CreateMap<Result, ResultResponseDto>().ConvertUsing((e, _, context) => ResultToResultResponseDto(e, context));
            CreateMap<ResultRequestDto, Result>().ConvertUsing((e, _, context) => ResultRequestDtoToResult(e));
            CreateMap<EmptyResultRequestDto, Result>().ConvertUsing((e, _, context) => EmptyResultRequestDtoToResult(e));
        }

        private ResultResponseDto ResultToResultResponseDto(Result result, ResolutionContext resolution)
        {
            return new ResultResponseDto
            {
                Hint = result.Answer.Component.Hint,
                Success = result.Answer.IsCorrectAnswer
            };
        }

        private Result ResultRequestDtoToResult(ResultRequestDto requestDto)
        {
            return new Result
            {
                StartTime = requestDto.StartTime,
                AnswerId = requestDto.AnswerId,
                UserId = requestDto.UserId ?? Guid.Empty,
                SessionId = requestDto.SessionId
            };
        }

        private Result EmptyResultRequestDtoToResult(EmptyResultRequestDto emptyRequestDto)
        {
            return new Result
            {
                StartTime = emptyRequestDto.StartTime,
                UserId = emptyRequestDto.UserId ?? Guid.Empty,
                SessionId = emptyRequestDto.SessionId,
                AnswerId = null
            };
            
        }

    }
}
