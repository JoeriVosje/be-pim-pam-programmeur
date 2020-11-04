using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public class ClassroomProcessor : IClassroomProcessor
    {
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepository;

        public ClassroomProcessor(IClassroomRepository classroomRepository, IMapper mapper)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
        }

        public async Task DeleteClassroom(Guid id)
        {
            await _classroomRepository.DeleteClassroom(id);
        }

        public IEnumerable<ClassroomResponseDto> GetAllClassrooms()
        {
            var classrooms = _classroomRepository.GetAllClassrooms();

            return _mapper.Map<IEnumerable<ClassroomResponseDto>>(classrooms);
            throw new NotImplementedException();
        }

        public ClassroomResponseDto GetClassroom(Guid id)
        {
            var classroom = _classroomRepository.GetClassroom(id);

            return classroom == null ? null : _mapper.Map<ClassroomResponseDto>(classroom);
        }

        public async Task<ClassroomResponseDto> SaveClassroom(ClassroomRequestDto classroomRequest)
        {
            var classroom = _mapper.Map<Classroom>(classroomRequest);
            var savedClassroom = await _classroomRepository.SaveClassroom(classroom);
            var classroomResponse =  _mapper.Map<ClassroomResponseDto>(savedClassroom);

            return classroomResponse;
        }
    }
}
