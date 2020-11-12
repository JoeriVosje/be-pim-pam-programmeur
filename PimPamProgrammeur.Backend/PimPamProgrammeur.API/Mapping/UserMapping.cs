using System;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<(string password, UserRequestDto), User>().ConvertUsing((e, _, context) => UserRequestDtoToUser(e));
            CreateMap<(string token, UserResponseDto), UserLoginResponseDto>().ConvertUsing((e, _, context) => UserResponseDtoToUserResponse(e));
            CreateMap<User, UserResponseDto>().ConvertUsing((e, _, context) => UserToUserResponseDto(e, context));
        }

        private UserLoginResponseDto UserResponseDtoToUserResponse((string token, UserResponseDto userDto) values)
        {
            if (values.userDto == null)
            {
                values.userDto = new UserResponseDto();
            }

            return new UserLoginResponseDto
            {
                AccessToken = values.token,
                Id = values.userDto.Id,
                RoleId = values.userDto.Role
            };
        }

        private UserResponseDto UserToUserResponseDto(User user, ResolutionContext context)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Classroom = context.Mapper.Map<ClassroomResponseDto>(user.ClassRoom), 
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
