
using System;

namespace PimPamProgrammeur.Dto
{
    public class UserLoginResponseDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Role id: 1 = Admin, 0 = User
        /// </summary>
        public int RoleId { get; set; }

        public string AccessToken { get; set; }

    }
}
