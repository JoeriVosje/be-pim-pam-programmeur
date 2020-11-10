using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsCorrectAnswer { get; set; }

        //Foreign key
        public Guid ComponentId { get; set; }

        //Navigation property
        public virtual Component Component { get; set; }

    }
}
