using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; } 
        [Required]
        public int Role { get; set; }
        [Required]
        public string CreationDate { get; set; }

        //Foreign key
        public Guid? ClassroomId { get; set; }

        //Navigation property
        public virtual Classroom ClassRoom { get; set; }

    }
}
