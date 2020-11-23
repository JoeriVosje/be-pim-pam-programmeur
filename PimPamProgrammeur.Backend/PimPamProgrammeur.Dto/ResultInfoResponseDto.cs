using System;
using PimPamProgrammeur.Model;

namespace PimPamProgrammeur.Dto
{
    public class ResultInfoResponseDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public UserResponseDto User { get; set; }

        public SessionResponseDto Session { get; set; }

        public AnswerResponseDto Answer { get; set; }
    }
}
