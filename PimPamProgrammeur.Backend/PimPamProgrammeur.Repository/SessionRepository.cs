using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

            return session;
        }

        public Session GetSession(Guid id)
        {
            return _context.Sessions.Include(x => x.Module).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Session> GetOpenSessions(Guid moduleId)
        {
            return _context.Sessions.Include(x => x.Module).Where(e => e.ModuleId == moduleId && e.EndTime == DateTime.MinValue);
        }

        public async Task<Session> UpdateSession(Session session)
        {
            _context.Entry(session).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return session;
        }

        public IEnumerable<Session> GetSessions()
        {
            return _context.Sessions.Include(x => x.Module).ToList();
        }
    }
}