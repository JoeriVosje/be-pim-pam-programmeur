using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class ResultOverview
    {
        public Guid QuestionId { get; set; } //Connection with the Component id?
        public Guid AnswerId { get; set; } //Connection with the Answer id?
    }
}
