using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class ComponentRequestDto
    {
        public ComponentRequestDto()
        {
            Answers = new List<AnswerRequestDto>();
        }

        public Guid ModuleId { get; set; }
        public string Title { get; set; }
        public string Theory { get; set; }
        public string Question { get; set; }
        public bool Skippable { get; set; }
        public string Hint { get; set; }
        public ICollection<AnswerRequestDto> Answers { get; set; }
    }
}
