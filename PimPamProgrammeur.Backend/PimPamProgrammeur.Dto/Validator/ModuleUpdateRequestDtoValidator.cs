using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ModuleUpdateRequestDtoValidator : Validator<ModuleUpdateRequestDto>
    {
        public override ValidationResult Validate(ModuleUpdateRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNull(validationResult, nameof(entity.Id), entity.Id);
            ValidateNullOrEmpty(validationResult, nameof(entity.Name), entity.Name);

            return validationResult;
        }
    }
}
