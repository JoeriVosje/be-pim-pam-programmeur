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
        private readonly IClassroomRepository _classRoomRepo;
        private readonly IResultRepository _resultRepo;
        private readonly IMapper _mapper;

        public SessionProcessor(ISessionRepository sessionRepository, IClassroomRepository classRoomRepo, IResultRepository resultRepo, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _classRoomRepo = classRoomRepo;
            _resultRepo = resultRepo;
            _mapper = mapper;
        }

        public SessionResponseDto GetSession(Guid sessionId)
        {
            var session = _sessionRepository.GetSession(sessionId);
            var (totalStudents, finishedStudents) = GetStudentStats(session);

            return _mapper.Map<SessionResponseDto>((totalStudents, finishedStudents, session));
        }

        private (int totalStudents, int finishedStudents) GetStudentStats(Session session)
        {
            var classRoom = _classRoomRepo.GetClassroomByModule(session.ModuleId);
            var totalStudents = classRoom.Users.Count;
            var componentCount = session.Module.Components.Count;

            var finishedStudents = 0;
            foreach (var classRoomUser in classRoom.Users)
            {
                var results = _resultRepo.FindResult(session.Id, classRoomUser.Id).ToList();
                if (results.Count == componentCount)
                {
                    finishedStudents++;
                }
            }

            return (totalStudents, finishedStudents);
        }

        public IEnumerable<SessionResponseDto> GetSessions()
        {
            var sessions = _sessionRepository.GetSessions();

            var toMap = sessions.Select((s) =>
            {
                var stats = GetStudentStats(s);
                return (stats.totalStudents, stats.finishedStudents, s);
            });

            return _mapper.Map<IEnumerable<SessionResponseDto>>(toMap);
        }

        public async Task<SessionResponseDto> Open(SessionRequestDto sessionRequest)
        {
            var sessionModel = _mapper.Map<Session>(sessionRequest);
            sessionModel.StartTime = DateTime.Now;

            var savedSession = await _sessionRepository.SaveSession(sessionModel);

            var (totalStudents, finishedStudents) = GetStudentStats(savedSession);

            return _mapper.Map<SessionResponseDto>((totalStudents, finishedStudents, savedSession));
        }

        public async Task<SessionResponseDto> Close(SessionRequestDto sessionRequest)
        {
            var session = GetCurrentSession(sessionRequest);

            session.EndTime = DateTime.Now;
            var updatedSession = await _sessionRepository.UpdateSession(session);

            var (totalStudents, finishedStudents) = GetStudentStats(updatedSession);

            return _mapper.Map<SessionResponseDto>((totalStudents, finishedStudents, updatedSession));
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
