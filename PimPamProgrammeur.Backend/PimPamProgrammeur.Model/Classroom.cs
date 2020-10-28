using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        //Foreign key
        public Guid ModuleId { get; set; }
        public Guid UserId { get; set; }

        //Navigation properties
        public Module Module { get; set; }
        public ICollection<User> Users { get; set; }
        
    }
}
