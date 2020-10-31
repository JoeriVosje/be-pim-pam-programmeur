using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ClassroomRequestDtoValidator : Validator<ClassroomRequestDto>
    {
        public override ValidationResult Validate(ClassroomRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNullOrEmpty(validationResult, nameof(entity.ModuleId), entity.ModuleId.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.Name), entity.Name);
            ValidateNullOrEmpty(validationResult, nameof(entity.Major), entity.Major);

            return validationResult;
        }
    }
}
