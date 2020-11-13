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
    public class ModuleRepository : IModuleRepository
    {
        private readonly PimPamProgrammeurContext _context;
        public ModuleRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public async Task<Module> SaveModule(Module module)
        {
            module.CreationDate = DateTime.Now;

            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();

            return module;
        }

        public Module GetModule(Guid id) 
        { 
            return _context.Modules.FirstOrDefault(e => e.Id == id); 
        }

        public async Task<Module> UpdateModule(Module module)
        {
            module.CreationDate = DateTime.Now;

            _context.Entry(module).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return module;
        }

        public IEnumerable<Module> GetModules()
        {
            return _context.Modules.OrderBy(e=> e.CreationDate).ToList();
        }
    }
}
