using System;

namespace PimPamProgrammeur.Dto
{
    public class UserRequestDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? ClassroomId { get; set; }
        public int Role { get; set; }

    }
}
