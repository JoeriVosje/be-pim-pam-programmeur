using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.API.Mapping
{
    public class SessionMapping : Profile
    {
        public SessionMapping()
        {
            CreateMap<SessionRequestDto, Session>().ConvertUsing(e => SessionRequestDtoToSession(e));
            CreateMap<Session, SessionResponseDto>().ConvertUsing((e, _, context) => SessionToSessionResponseDto(e, context));
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
