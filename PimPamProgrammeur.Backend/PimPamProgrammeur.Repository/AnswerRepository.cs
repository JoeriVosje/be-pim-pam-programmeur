using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PimPamProgrammeur.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public AnswerRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public IEnumerable<Answer> DeleteAnswersByComponentId(Guid componentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Answer> GetAnswersByComponentId(Guid componentId)
        {
            return _context.Answers.Where(e => e.ComponentId == componentId);
        }


    }
}
