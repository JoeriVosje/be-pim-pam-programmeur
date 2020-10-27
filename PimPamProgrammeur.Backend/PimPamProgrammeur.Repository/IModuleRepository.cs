using PimPamProgrammeur.Model;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IModuleRepository
    {
        Task<Module> SaveModule(Module module);
    }
}