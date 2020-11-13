using System;
using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.API.Mapping
{
    public class SessionMapping : Profile
    {
        public SessionMapping()
        {
            CreateMap<Session, SessionResponseDto>().ConvertUsing((e, _, context) => SessionToSessionResponseDto(e, context));
            CreateMap<SessionRequestDto, Session>().ConvertUsing(e => SessionRequestDtoToSession(e));
            CreateMap<(int totalStudents, int finishedStudents, Session model), SessionResponseDto>().ConvertUsing((e, _, context) => SessionWithStatsToSessionResponseDto(e, context));
        }

        private SessionResponseDto SessionWithStatsToSessionResponseDto((int totalStudents, int finishedStudents, Session session) data, ResolutionContext context)
        {
            return new SessionResponseDto
            {
                SessionId = data.session.Id,
                StartTime = data.session.StartTime,
                EndTime = data.session.EndTime,
                Module = context.Mapper.Map<ModuleResponseDto>(data.session.Module),
                StudentsFinished = data.finishedStudents,
                TotalStudents = data.totalStudents
            };
        }

        private SessionResponseDto SessionToSessionResponseDto(Session session, ResolutionContext context)
        {
            return new SessionResponseDto
            {
                SessionId = session.Id,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Module = context.Mapper.Map<ModuleResponseDto>(session.Module)
            };
        }

        private Session SessionRequestDtoToSession(SessionRequestDto sessionRequestDto)
        {
            return new Session
            {
                ModuleId = sessionRequestDto.ModuleId
            };
        }
    }
}
