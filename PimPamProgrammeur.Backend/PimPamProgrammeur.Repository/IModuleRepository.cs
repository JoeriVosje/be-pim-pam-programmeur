using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IModuleRepository
    {
        Task<Module> SaveModule(Module module);
        Module GetModule(Guid id);
        Task<Module> UpdateModule(Module module);
        IEnumerable<Module> GetModules();
        Task DeleteModule(Guid id);

    }
}