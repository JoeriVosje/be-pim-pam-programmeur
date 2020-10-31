using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class ClassroomRequestDto
    {
        public string Name { get; set; }
        public Guid ModuleId { get; set; }
        public string Major { get; set; }
    }
}
