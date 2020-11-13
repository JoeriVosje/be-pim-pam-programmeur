using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IComponentRepository
    {
        Task<Component> SaveComponent(Component component);
        Component GetComponent(Guid id);
        IEnumerable<Component> GetComponentsByModule(Guid ModuleId);
        IEnumerable<Component> GetComponents();
        Task DeleteComponent(Guid id);
        Task<Component> UpdateComponent(Component component);
        Task<Component> SetOrder(int i, Guid componentId);
    }
}
