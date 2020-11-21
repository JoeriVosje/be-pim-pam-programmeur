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
        private readonly IResultRepository _resultRepository;

        public ClassroomProcessor(IClassroomRepository classroomRepository, IMapper mapper, IUserRepository userRepository, IResultRepository resultRepository)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _resultRepository = resultRepository;
        }

        public async Task DeleteClassroom(Guid id)
        {
            if (_classroomRepository.GetClassroom(id) != null)
            {
                var getAllUsers = _userRepository.GetUserByClassroomId(id);

                if (getAllUsers.Any())
                {
                    var usersCantBeDeleted = CheckIfUsersCanBeDeleted(getAllUsers);
                    if (usersCantBeDeleted)
                    {
                        throw new ArgumentException("There are users with results, first delete the results");
                    }
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

        public bool CheckIfUsersCanBeDeleted(IEnumerable<User> users)
        {
            var usersHasResults = false;
            foreach (var user in users)
            {
               var hasResults = _resultRepository.GetByUserId(user.Id);
                if (hasResults.Any())
                {
                    usersHasResults = true;
                    return usersHasResults;
                }
            }
            return usersHasResults;
        }
    }
}
