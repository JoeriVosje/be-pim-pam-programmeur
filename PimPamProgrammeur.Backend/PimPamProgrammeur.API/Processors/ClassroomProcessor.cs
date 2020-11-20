using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public class ClassroomProcessor : IClassroomProcessor
    {
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserRepository _userRepository;

        public ClassroomProcessor(IClassroomRepository classroomRepository, IMapper mapper, IUserRepository userRepository)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task DeleteClassroom(Guid id)
        {
            if (_classroomRepository.GetClassroom(id) != null)
            {
                var getAllUsers = _userRepository.GetUserByClassroomId(id);
                if (getAllUsers.Any())
                {
                    await _userRepository.DeleteAllUsers(getAllUsers);
                }
                await _classroomRepository.DeleteClassroom(id);
            }
        }

        public IEnumerable<ClassroomResponseDto> GetAllClassrooms()
        {
            var classrooms = _classroomRepository.GetAllClassrooms();

            return _mapper.Map<IEnumerable<ClassroomResponseDto>>(classrooms);
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
