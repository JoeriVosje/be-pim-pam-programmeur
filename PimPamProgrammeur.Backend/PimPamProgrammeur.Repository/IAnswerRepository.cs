using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Repository
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAnswersByComponentId(Guid componentId);
    }
}
