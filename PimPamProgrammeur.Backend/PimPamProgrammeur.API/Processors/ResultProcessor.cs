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

        public ResultProcessor(IResultRepository resultRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
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
    }
}
