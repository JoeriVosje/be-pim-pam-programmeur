using System;

namespace PimPamProgrammeur.Dto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ClassroomResponseDto Classroom { get; set; }
        public int Role { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
