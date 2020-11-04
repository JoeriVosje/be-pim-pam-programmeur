using PimPamProgrammeur.Data;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _context.Classrooms.ToList();
        }

        public Classroom GetClassroom(Guid id)
        {
            return _context.Classrooms.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Classroom> SaveClassroom(Classroom classroom)
        {
            await _context.Classrooms.AddAsync(classroom);
            await _context.SaveChangesAsync();

            return classroom;
        }
    }
}
