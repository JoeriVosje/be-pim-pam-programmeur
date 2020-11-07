using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ComponentUpdateRequestDtoValidator : Validator<ComponentUpdateRequestDto>
    {
        public override ValidationResult Validate(ComponentUpdateRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNullOrEmpty(validationResult, nameof(entity.Id), entity.Id.ToString());

            return validationResult;
        }
    }
}
