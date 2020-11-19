using System;

namespace PimPamProgrammeur.Dto
{
    public class ModuleResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }
}
