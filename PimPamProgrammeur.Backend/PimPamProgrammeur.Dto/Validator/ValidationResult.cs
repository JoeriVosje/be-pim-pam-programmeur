using System.Collections.Generic;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ValidationResult
    {
        public List<string> Errors { get; } = new List<string>();
    }
}
