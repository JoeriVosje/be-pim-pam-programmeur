using Microsoft.AspNetCore.Authorization;
using PimPamProgrammeur.Utils;

namespace PimPamProgrammeur.API.Auth
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminAttribute()
        {
            Policy = Constants.Admin;
            AuthenticationSchemes = Constants.TokenAuthenticationScheme;
        }
    }
}