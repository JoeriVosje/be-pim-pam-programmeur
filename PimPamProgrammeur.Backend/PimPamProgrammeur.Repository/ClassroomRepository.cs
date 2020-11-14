using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace PimPamProgrammeur.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly PimPamProgrammeurContext _context;

        public ClassroomRepository(PimPamProgrammeurContext context)
        {
            _context = context;
        }

        public async Task DeleteClassroom(Guid id)
        {
            var classroom = GetClassroom(id);
            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Classroom> GetAllClassrooms()
        {
            return GetClassrooms().OrderBy(e=> e.CreationDate).ToList();
        }

        public Classroom GetClassroomByModule(Guid moduleId)
        {
            return GetClassrooms().FirstOrDefault(e => e.ModuleId == moduleId);
        }

        private IIncludableQueryable<Classroom, Module> GetClassrooms()
        {
            return _context.Classrooms.Include(e => e.Module);
        }

        public Classroom GetClassroom(Guid id)
        {
            return GetClassrooms().FirstOrDefault(e => e.Id == id);
        }

        public async Task<Classroom> SaveClassroom(Classroom classroom)
        {
            await _context.Classrooms.AddAsync(classroom);
            await _context.SaveChangesAsync();

            return classroom;
        }
    }
}
