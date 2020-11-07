using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class SessionResponseDto
    {
        public Guid SessionId { get; set; }

        public ModuleResponseDto Module { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
