using System;
using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<(string password, UserRequestDto), User>().ConvertUsing(e => UserRequestDtoToUser(e));
            CreateMap<User, UserResponseDto>().ConvertUsing(e => UserToUserResponseDto(e));
        }

        private UserResponseDto UserToUserResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Classroom = null, // TODO as soon as classRepo is implemented
                CreationDate = user.CreationDate,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        private User UserRequestDtoToUser((string password, UserRequestDto userRequestDto) user)
        {
            var (password, userRequestDto) = user;
            return new User
            {
                ClassroomId = userRequestDto.ClassroomId,
                CreationDate = DateTime.Now,
                Email = userRequestDto.Email,
                Password = password,
                FirstName = userRequestDto.FirstName,
                LastName = userRequestDto.LastName,
                Role = userRequestDto.Role
            };
        }
    }
}
