﻿using PimPamProgrammeur.Data;
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

        public Answer GetAnswer(Guid id)
        {
            return _context.Answers.FirstOrDefault( e => e.Id == id);
        }

        public async Task DeleteAnswersByComponentId(Guid componentId)
        {
            var answers = GetAnswersByComponentId(componentId);
            foreach (var answer in answers)
            {
                _context.Answers.Remove(answer);
            }

            await _context.SaveChangesAsync();

        }
        public async Task<Answer> UpdateAnswerByComponentId(Answer answer)
        {
            await _context.SaveChangesAsync();

            return answer;
        }

        public Answer GetRightAnswerByComponentId(Guid componentId)
        {
            return _context.Answers.Where(e => e.ComponentId == componentId && e.IsCorrectAnswer == true).FirstOrDefault();
             
        }
    }
}
