using System;
using System.ComponentModel.DataAnnotations.Schema;
using Type = PimPamProgrammeur.Model.Enums.Type;

namespace PimPamProgrammeur.Model
{
    public class ComponentInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Type Type { get; set; }
    }
}
