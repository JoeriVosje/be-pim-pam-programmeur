using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class Component
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Module")]
        public string ModuleId { get; set; } //Connection with Module (Guid id of class Module)
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
        [Required]
        public Answer[] Answers { get; set; } // List<Answer> ?
    }
}
