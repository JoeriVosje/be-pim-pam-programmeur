using PimPamProgrammeur.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IResultProcessor
    {
        IEnumerable<ResultResponseDto> FindResults(Guid sessionId, Guid userId);
        Task<ResultResponseDto> SaveResult(ResultRequestDto resultRequest);

        Task<AnswerResponseDto> SaveEmptyResult(EmptyResultRequestDto request);
    }
}