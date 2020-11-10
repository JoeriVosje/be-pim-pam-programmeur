using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ComponentRequestDtoValidator : Validator<ComponentRequestDto>
    {
        public override ValidationResult Validate(ComponentRequestDto entity)
        {
            var validationResult = new ValidationResult();

            // Check all required fields
            ValidateNullOrEmpty(validationResult, nameof(entity.Title), entity.Title);
            ValidateNullOrEmpty(validationResult, nameof(entity.Theory), entity.Theory);
            ValidateNullOrEmpty(validationResult, nameof(entity.Question), entity.Question);
            ValidateNullOrEmpty(validationResult, nameof(entity.Skippable), entity.Skippable.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.Hint), entity.Hint);
            ValidateNullOrEmpty(validationResult, nameof(entity.ModuleId), entity.ModuleId.ToString());

            return validationResult;
        }
    }
}
