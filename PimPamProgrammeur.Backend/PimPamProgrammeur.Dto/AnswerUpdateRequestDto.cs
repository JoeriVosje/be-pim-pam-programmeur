using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class AnswerUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ComponentId { get; set; }
    }
}
