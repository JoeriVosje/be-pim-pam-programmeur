using PimPamProgrammeur.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public interface IClassroomProcessor
    {
        Task<ClassroomResponseDto> SaveClassroom(ClassroomRequestDto classroomRequest);
        Task DeleteClassroom(Guid id);
        ClassroomResponseDto GetClassroom(Guid id);
        IEnumerable<ClassroomResponseDto> GetAllClassrooms();
    }
}
