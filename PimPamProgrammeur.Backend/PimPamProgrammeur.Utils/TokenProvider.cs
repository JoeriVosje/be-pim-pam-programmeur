

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PimPamProgrammeur.Dto;

namespace PimPamProgrammeur.Utils
{
    public class TokenProvider : ITokenProvider
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly SymmetricSecurityKey _securityKey = 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.Secret));

        public TokenProvider(JwtSecurityTokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        public string GenerateToken(UserResponseDto dto)
        {
            var securityToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(BuildClaims(dto)),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(securityToken);
            return _tokenHandler.WriteToken(token);
        }

        public (bool isValid, IEnumerable<Claim> claims) ReadToken(string token)
        {
            token = token.Replace("Bearer", "").Trim();
            var securityToken = GetSecurityToken(token);
            if (securityToken is JwtSecurityToken jwtToken && 
                DateTime.UtcNow < jwtToken.ValidTo)
            {
                return (true, jwtToken.Claims);
            }

            return (false, null);
        }

        public Guid? GetUserId(string token)
        {
            var (isValid, claims) = this.ReadToken(token);
            if (!isValid)
            {
                return null;
            }

            var userIdClaim
                = claims.FirstOrDefault(e => e.Type == Constants.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value) || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return null;
            }

            return userId;
        }

        private SecurityToken GetSecurityToken(string token)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = _securityKey
            };

            try
            {
                _tokenHandler.ValidateToken(token, tokenValidationParams, out var validationToken);
                return validationToken;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }

        private IEnumerable<Claim> BuildClaims(UserResponseDto dto)
        {
            return new List<Claim>
            {
                new Claim(Constants.UserId, dto.Id.ToString()),
                new Claim(Constants.RoleId, dto.Role.ToString()),
            };
        }
    }
}
