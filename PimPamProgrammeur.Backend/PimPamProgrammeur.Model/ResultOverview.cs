using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class ResultOverview
    {
        [ForeignKey("Component")]
        public Guid QuestionId { get; set; } //Connection with the Component id?
        [ForeignKey("Answer")]
        public Guid AnswerId { get; set; } //Connection with the Answer id?
    }
}
