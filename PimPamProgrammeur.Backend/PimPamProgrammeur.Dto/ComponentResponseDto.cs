using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class ComponentResponseDto
    {
        public ComponentResponseDto()
        {
            Answers = new List<AnswerResponseDto>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theory { get; set; }
        public string Question { get; set; }
        public bool Skippable { get; set; }
        public string Hint { get; set; }
        public Guid ModuleId { get; set; }

        public ICollection<AnswerResponseDto> Answers { get; set; }
    }
}
