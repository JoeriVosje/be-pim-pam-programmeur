using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimPamProgrammeur.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; } 
        public Classroom ClassRoom { get; set; } //ClassRoom conform the Swaggerhub name
        [Required]
        public int Role { get; set; }
        [Required]
        public string CreationDate { get; set; }

    }
}
