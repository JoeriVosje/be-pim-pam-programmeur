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
    public class ResultProcessor : IResultProcessor
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;

        public ResultProcessor(IResultRepository resultRepository, IMapper mapper, IAnswerRepository answerRepository)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
            _answerRepository = answerRepository;
        }
        public IEnumerable<ResultResponseDto> FindResults(Guid sessionId, Guid userId)
        {
            var result = _resultRepository.FindResult(sessionId, userId);

            return _mapper.Map<IEnumerable<ResultResponseDto>>(result);
        }

        public async Task<ResultResponseDto> SaveResult(ResultRequestDto resultRequest)
        {
            var result = _mapper.Map<Result>(resultRequest);
            var savedResult = await _resultRepository.SaveResult(result);
            var resultResponse = _mapper.Map<ResultResponseDto>(savedResult);

            return resultResponse;
        }
        public async Task<AnswerResponseDto> SaveEmptyResult(EmptyResultRequestDto request)
        {
            var result = _mapper.Map<Result>(request);
            await _resultRepository.SaveResult(result);

            //Get right answer
            var rightAnswer = _answerRepository.GetRightAnswerByComponentId(request.ComponentId);

            var answerResponse = _mapper.Map<AnswerResponseDto>(rightAnswer);

            return answerResponse;
        }


    }
}
