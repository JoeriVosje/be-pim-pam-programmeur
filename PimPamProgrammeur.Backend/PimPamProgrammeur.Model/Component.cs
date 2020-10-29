using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class Component
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Theory { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public bool Skippable { get; set; }
        [Required]
        public string Hint { get; set; }

        //Foreign 
        public Guid ModuleId { get; set; }
        public Guid CorrectAnswerId { get; set; }

        //Navigation property
        public virtual Module Module { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
