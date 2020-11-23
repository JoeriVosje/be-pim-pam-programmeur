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
            CreateMap<Answer, ResultResponseDto>().ConvertUsing((e, _) => CorrectAnswerToResultResponseDto(e));
            CreateMap<Result, ResultInfoResponseDto>().ConvertUsing((e, _, context) => ResultToResultInfoResponseDto(e, context));
        }

        private ResultInfoResponseDto ResultToResultInfoResponseDto(Result result, ResolutionContext context)
        {
            return new ResultInfoResponseDto
            {
                Id = result.Id,
                Answer = context.Mapper.Map<AnswerResponseDto>(result.Answer),
                Session = context.Mapper.Map<SessionResponseDto>(result.Session),
                User = context.Mapper.Map<UserResponseDto>(result.User),
                StartTime = result.StartTime,
                EndTime = result.EndTime
            };
        }

        private ResultResponseDto CorrectAnswerToResultResponseDto(Answer answer)
        {
            return new ResultResponseDto
            {
                CorrectAnswerId = answer.Id,
                Hint = answer.Component.Hint,
                Success = false // Always false, because the user chose to skip the component
            };

        }

        private ResultResponseDto ResultToResultResponseDto(Result result, ResolutionContext resolution)
        {
            return new ResultResponseDto
            {
                Hint = result.Answer.Component.Hint,
                Success = result.Answer.IsCorrectAnswer,
                CorrectAnswerId = result.Answer.Id
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
