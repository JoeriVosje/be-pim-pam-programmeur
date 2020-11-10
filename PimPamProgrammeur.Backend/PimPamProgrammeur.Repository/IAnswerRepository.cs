using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAnswersByComponentId(Guid componentId);
        Task DeleteAnswersByComponentId(Guid componentId);
        Task<Answer> UpdateAnswerByComponentId(Answer answer);

    }
}
