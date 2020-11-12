using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IResultRepository
    {
        Task<Result> SaveResult(Result result);
        IEnumerable<Result> FindResult(Guid sessionId, Guid userId);
    }
}