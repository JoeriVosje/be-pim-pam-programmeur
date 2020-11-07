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

        public Component GetComponentByModule(Guid ModuleId)
        {
            return _context.Components.FirstOrDefault(m => m.ModuleId == ModuleId);
        }

        public async Task<Component> SaveComponent(Component component)
        {
            await _context.Components.AddAsync(component);
            await _context.SaveChangesAsync();

            return component;
        }

        public async Task<Component> UpdateComponent(Component component)
        {
            _context.Entry(component).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return component;
        }
    }
}
