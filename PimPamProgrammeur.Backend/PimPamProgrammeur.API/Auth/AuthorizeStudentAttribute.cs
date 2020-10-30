using Microsoft.AspNetCore.Authorization;
using PimPamProgrammeur.Utils;

namespace PimPamProgrammeur.API.Auth
{
    public class AuthorizeStudentAttribute : AuthorizeAttribute
    {
        public AuthorizeStudentAttribute()
        {
            Policy = Constants.Student;
            AuthenticationSchemes = Constants.TokenAuthenticationScheme;
        }
    }
}
