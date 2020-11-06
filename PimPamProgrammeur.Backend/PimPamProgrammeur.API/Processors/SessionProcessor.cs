using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.OpenApi.Validations;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.API.Processors
{
    public class SessionProcessor : ISessionProcessor
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public SessionProcessor(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public SessionResponseDto GetSession(Guid sessionId)
        {
            var session = _sessionRepository.GetSession(sessionId);

            return _mapper.Map<SessionResponseDto>(session);
        }

        public IEnumerable<SessionResponseDto> GetSessions()
        {
            var session = _sessionRepository.GetSessions();

            return _mapper.Map<IEnumerable<SessionResponseDto>>(session);
        }

        public async Task<SessionResponseDto> Open(SessionRequestDto sessionRequest)
        {
            var sessionModel = _mapper.Map<Session>(sessionRequest);
            sessionModel.StartTime = DateTime.Now;

            var savedSession = await _sessionRepository.SaveSession(sessionModel);

            return _mapper.Map<SessionResponseDto>(savedSession);
        }

        public async Task<SessionResponseDto> Close(SessionRequestDto sessionRequest)
        {
            var session = GetCurrentSession(sessionRequest);

            session.EndTime = DateTime.Now;
            var updatedSession = await _sessionRepository.UpdateSession(session);

            return _mapper.Map<SessionResponseDto>(updatedSession);
        }

        private Session GetCurrentSession(SessionRequestDto sessionRequest)
        {
            var sessions = _sessionRepository.GetOpenSessions(sessionRequest.ModuleId).ToList();
            if (sessions.Count > 1)
            {
                throw new Exception($"Multiple open session for {sessionRequest.ModuleId} found!");
            }

            if (sessions.Count == 0)
            {
                throw new Exception($"No open session for {sessionRequest.ModuleId} found!");
            }

            return sessions.First();
            
        }
    }
}
