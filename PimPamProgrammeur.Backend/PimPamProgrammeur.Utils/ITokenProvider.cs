using System.Collections.Generic;
using System.Security.Claims;
using PimPamProgrammeur.Dto;

namespace PimPamProgrammeur.Utils
{
    public interface ITokenProvider
    {
        string GenerateToken(UserResponseDto dto);
        (bool isValid, IEnumerable<Claim> claims) ReadToken(string token);
    }
}