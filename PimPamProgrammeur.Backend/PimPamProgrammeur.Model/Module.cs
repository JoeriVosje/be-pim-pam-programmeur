using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace PimPamProgrammeur.Model
{
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        //Navigation property
        public virtual ICollection<Component> Components { get; set; }
        public virtual Session Session { get; set; }
    }
}
