using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class SessionStatusResponseDto
    {
        public Guid SessionId { get; set; }

        public ComponentResponseDto LastAnsweredComponent { get; set; }

        public bool Finished { get; set; }
    }
}
