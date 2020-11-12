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

            //If SavedResult has isSkipped get the right Answer
            if (savedResult.HasSkipped == true)
            {
                var getRightAnswer = _answerRepository.GetAnswersById(savedResult.AnswerId);
                savedResult.AnswerId = getRightAnswer.Id;
                savedResult.Answer = getRightAnswer;
                var RightResult = _mapper.Map<ResultResponseDto>(savedResult);
                return RightResult;

            }

            var resultResponse = _mapper.Map<ResultResponseDto>(savedResult);

            return resultResponse;
        }
    }
}
