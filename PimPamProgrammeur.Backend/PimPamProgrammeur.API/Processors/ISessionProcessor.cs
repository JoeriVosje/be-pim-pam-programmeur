using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PimPamProgrammeur.Dto;

namespace PimPamProgrammeur.API.Processors
{
    public interface ISessionProcessor
    {
        SessionResponseDto GetSession(Guid sessionId);
        IEnumerable<SessionResponseDto> GetSessions(Guid? moduleId);
        Task<SessionResponseDto> Open(SessionRequestDto sessionRequest);
        Task<SessionResponseDto> Close(SessionRequestDto sessionRequest);
    }
}