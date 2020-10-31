using PimPamProgrammeur.Model;
using System;
using System.Threading.Tasks;

namespace PimPamProgrammeur.Repository
{
    public interface IClassroomRepository
    {
        Task<Classroom> SaveClassroom(Classroom classroom);
        Task DeleteClassroom(Guid id);
        Classroom GetClassroom(Guid id);
    }
}
