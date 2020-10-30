using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PimPamProgrammeur.Dto;

namespace PimPamProgrammeur.API.Processors
{
    public interface IUserProcessor
    {
        Task<UserResponseDto> SaveUser(UserRequestDto userRequest);
        Task DeleteUser(Guid id);
        IEnumerable<UserResponseDto> GetUsers();
        UserResponseDto GetUser(Guid id);
        string Login(UserLoginRequestDto userLoginRequestDto);
    }
}