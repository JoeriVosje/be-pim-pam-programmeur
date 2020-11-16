using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly PimPamProgrammeurContext _context;
        public SessionRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public async Task<Session> SaveSession(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return _context.Sessions.Include(e => e.Module).First(id => id.Id == session.Id);
        }

        public Session GetSession(Guid id)
        {
            return GetSessionAndModule().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Session> GetOpenSessions(Guid moduleId)
        {
            return GetSessionAndModule().Where(e => e.ModuleId == moduleId && e.EndTime == null);
        }

        public async Task<Session> UpdateSession(Session session)
        {
            _context.Entry(session).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return session;
        }


        public IEnumerable<Session> GetSessions(Guid? moduleId)
        {
            return moduleId.HasValue ?
                GetSessionAndModule().Where(e=> e.ModuleId == moduleId.Value).OrderBy(e => e.StartTime).ToList() :
                GetSessionAndModule().OrderBy(e=> e.StartTime).ToList();
        }
        private IIncludableQueryable<Session, Module> GetSessionAndModule()
        {
            return _context.Sessions.Include(x => x.Module); 
        }
    }
}