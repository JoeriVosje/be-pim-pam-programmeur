using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.Repository
{
    public interface ISessionRepository
    {
        Task<Session> SaveSession(Session session);
        Session GetSession(Guid id);
        IEnumerable<Session> GetOpenSessions(Guid moduleId);
        Task<Session> UpdateSession(Session session);
        IEnumerable<Session> GetSessions();
    }
}