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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SessionProcessor(ISessionRepository sessionRepository, IClassroomRepository classRoomRepo, IResultRepository resultRepo, IUserRepository userRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _classRoomRepo = classRoomRepo;
            _resultRepo = resultRepo;
            _userRepository = userRepository;
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
            if (classRoom == null)
            {
                return (0, 0);
            }
            var totalStudents = classRoom.Users.Count;
            var componentCount = session.Module.Components.Count;

            var finishedStudents = 0;
            foreach (var classRoomUser in classRoom.Users)
            {
                var results = _resultRepo.FindResult(session.Id, classRoomUser.Id).ToList();
                if (results.Count >= componentCount)
                {
                    finishedStudents++;
                }
            }

            return (totalStudents, finishedStudents);
        }

        public IEnumerable<SessionResponseDto> GetSessions(Guid? moduleId)
        {
            var sessions = _sessionRepository.GetSessions(moduleId);

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
            sessionModel.StartTime = DateTime.UtcNow.FromUtcToGmt();

            var savedSession = await _sessionRepository.SaveSession(sessionModel);

            var (totalStudents, finishedStudents) = GetStudentStats(savedSession);

            return _mapper.Map<SessionResponseDto>((totalStudents, finishedStudents, savedSession));
        }

        public async Task<SessionResponseDto> Close(SessionRequestDto sessionRequest)
        {
            var session = GetCurrentSession(sessionRequest);

            session.EndTime = DateTime.UtcNow.FromUtcToGmt();
            var updatedSession = await _sessionRepository.UpdateSession(session);

            var (totalStudents, finishedStudents) = GetStudentStats(updatedSession);

            return _mapper.Map<SessionResponseDto>((totalStudents, finishedStudents, updatedSession));
        }

        public SessionStatusResponseDto GetSessionStatus(Guid userId)
        {
            var user = _userRepository.GetUser(userId);

            if (user.ClassRoom == null)
            {
                return null;
            }

            var sessions = _sessionRepository.GetOpenSessions(user.ClassRoom.ModuleId);

            var activeSession = sessions.FirstOrDefault();
            if (activeSession == null)
            {
                return null;
            }

            var results = _resultRepo.FindResult(activeSession.Id, userId);

            var components = activeSession.Module.Components.OrderBy(e=> e.Order).ToDictionary((c) => c.Id, c => c);
            var lastComponentOfModule = components.OrderByDescending(e => e.Value.Order).FirstOrDefault();
            var lastResultOfUser = results.OrderByDescending(e => GetComponentFromResult(e).Order).FirstOrDefault();

            var lastAnsweredComponent = GetComponentFromResult(lastResultOfUser, components);
            if (lastAnsweredComponent == null)
            {
                return null;
            }

            return new SessionStatusResponseDto()
            {
                SessionId = activeSession.Id,
                LastAnsweredComponent = _mapper.Map<ComponentResponseDto>(lastAnsweredComponent),
                Finished = lastAnsweredComponent.Id == lastComponentOfModule.Key
            };
        }

        private static Component GetComponentFromResult(Result r)
        {
            if (r.Answer != null)
            {
                return r.Answer.Component;
            }

            return r.Component;
        }

        private static Component GetComponentFromResult(Result lastResultOfUser, Dictionary<Guid, Component> components)
        {
            if (lastResultOfUser == null || lastResultOfUser.Answer == null && lastResultOfUser.Component == null)
            {
                return null;
            }

            var currentComponentId = lastResultOfUser.Answer != null
                ? lastResultOfUser.Answer.ComponentId
                : lastResultOfUser.Component.Id;

            return components.ContainsKey(currentComponentId) ? components[currentComponentId] : null;
        }

        private SessionStatusResponseDto GetSessionStatus(User user)
        {
            return null;
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
