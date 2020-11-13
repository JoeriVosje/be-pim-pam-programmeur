using System;

namespace PimPamProgrammeur.Dto
{
    public class EmptyResultRequestDto
    {
        public Guid? UserId { get; set; }
        public Guid ComponentId { get; set; }
        public Guid SessionId { get; set; }
        public DateTime StartTime { get; set; }

    }
}
