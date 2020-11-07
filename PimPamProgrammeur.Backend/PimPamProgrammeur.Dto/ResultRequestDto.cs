using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class ResultRequestDto
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
    