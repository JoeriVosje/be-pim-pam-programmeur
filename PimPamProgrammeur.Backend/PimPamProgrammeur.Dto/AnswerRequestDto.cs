using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class AnswerRequestDto
    {
        public string Description { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
