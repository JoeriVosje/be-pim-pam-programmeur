using Microsoft.EntityFrameworkCore;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public ResultRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }
        public IEnumerable<Result> FindResult(Guid sessionId, Guid userId)
        {
            return _context.Results.Where(e => e.SessionId == sessionId && e.UserId == userId);
        }

        public async Task<Result> SaveResult(Result result)
        {
            result.EndTime = DateTime.Now;

            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
