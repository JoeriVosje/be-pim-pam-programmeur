using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool Succes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Foreign key
        public Guid AnswerId { get; set; }
        public Guid UserId { get; set; }

        //Navigation property
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
    }
}
