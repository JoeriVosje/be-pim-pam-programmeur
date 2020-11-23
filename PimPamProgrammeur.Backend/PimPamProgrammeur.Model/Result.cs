using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Foreign key
        public Guid? AnswerId { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public Guid? ComponentId { get; set; }

        //Navigation property
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual Component Component { get; set; }
    }
}
