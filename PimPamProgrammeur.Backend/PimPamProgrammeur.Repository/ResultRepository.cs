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

            return _context.Results.Include(e => e.Answer).First(e => e.Id == result.Id);
        }

        public bool CheckIfResultsArePresentByUserId(Guid userId)
        {
            var results = _context.Results.Where(e => e.UserId == userId).ToList();
            if (results.Any())
            {
                return true;
            }
            return false;
        }
    }
}
