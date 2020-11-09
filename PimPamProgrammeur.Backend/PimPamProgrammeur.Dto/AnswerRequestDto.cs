using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class AnswerRequestDto
    {
        [Required]
        public string Description { get; set; }
        public Guid ComponentId { get; set; }
        [Required]
        public bool IsCorrectAnswer { get; set; }
    }
}
