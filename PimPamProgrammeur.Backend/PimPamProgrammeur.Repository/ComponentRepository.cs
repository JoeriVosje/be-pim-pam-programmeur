using Microsoft.EntityFrameworkCore;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public ComponentRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public async Task DeleteComponent(Guid id)
        {
            var component = GetComponent(id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
        }

        public Component GetComponent(Guid id)
        {
            return _context.Components.FirstOrDefault(e => e.Id == id);
        }



        public IEnumerable<Component> GetComponentsByModule(Guid ModuleId)
        {
            return _context.Components.Where(m => m.ModuleId == ModuleId).OrderBy(e => e.Order).ToList();
        }

        public IEnumerable<Component> GetComponents()
        {
            return _context.Components.OrderBy(e => e.Order).ToList();
        }

        public async Task<Component> SaveComponent(Component component)
        {
            var highestOrderComponent = GetComponentsByModule(component.ModuleId)
                .OrderByDescending(e => e.Order).FirstOrDefault();

            component.Order = highestOrderComponent?.Order ?? 0;
            await _context.Components.AddAsync(component);
            await _context.SaveChangesAsync();

            return component;
        }

        public async Task<Component> UpdateComponent(Component component)
        {
            await _context.SaveChangesAsync();

            return component;
        }

        public async Task<Component> SetOrder(int order, Guid componentId)
        {
            var component = GetComponent(componentId);
            component.Order = order;

            _context.Entry(component).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            

            return component;
        }
    }
}
