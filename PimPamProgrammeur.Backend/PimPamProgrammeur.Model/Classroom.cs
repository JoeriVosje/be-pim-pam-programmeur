using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class Classroom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Major { get; set; }

        public DateTime CreationDate { get; set; }

        //Foreign key
        public Guid ModuleId { get; set; }

        //Navigation properties
        public virtual Module Module { get; set; }
        public virtual ICollection<User> Users { get; set; }
        
    }
}
