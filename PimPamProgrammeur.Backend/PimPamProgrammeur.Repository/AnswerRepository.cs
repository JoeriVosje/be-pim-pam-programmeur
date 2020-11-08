using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public AnswerRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public IEnumerable<Answer> GetAnswersByComponentId(Guid componentId)
        {
            return _context.Answers.Where(e => e.ComponentId == componentId);
        }

        public async Task DeleteAnswersByComponentId(Guid componentId)
        {
            var answers = GetAnswersByComponentId(componentId);
            foreach (var answer in answers)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
