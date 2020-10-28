using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class ResultOverview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public Guid AnswerId { get; set; }

        //Navigation properties
        public Answer Answer { get; set; }
        public Component Component { get; set; }
    }
}
