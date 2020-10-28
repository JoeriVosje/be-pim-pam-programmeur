using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class Session
    {
        [Required]
        [ForeignKey("Module")]
        public Guid ModuleId { get; set; } //Connection with the Module id
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
