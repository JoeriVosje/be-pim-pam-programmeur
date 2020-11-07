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
        }

        private ResultResponseDto ResultToResultResponseDto(Result result, ResolutionContext resolution)
        {
            return new ResultResponseDto
            {
                //CorrectAnswerId
                // Hint 
                //Success
            };
        }

        private Result ResultRequestDtoToResult(ResultRequestDto requestDto)
        {
            return new Result
            {
                StartTime = requestDto.StartTime,
                AnswerId = requestDto.AnswerId,
                UserId = requestDto.UserId
            };
        }
    }
}
