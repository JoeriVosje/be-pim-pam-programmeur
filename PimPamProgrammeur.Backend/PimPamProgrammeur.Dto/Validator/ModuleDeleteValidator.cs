using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ModuleDeleteValidator : Validator<Classroom>
    {
        public override ValidationResult Validate(Classroom entity)
        {
            var validationResult = new ValidationResult();
            if(entity.Id != null)
            {
                validationResult.Errors.Add($"Classroom is not null");
            }

            return validationResult;
        }
    }
}
