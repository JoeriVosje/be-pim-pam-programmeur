using System;

namespace PimPamProgrammeur.Dto
{
    public class ClassroomResponseDto
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
    }
}
