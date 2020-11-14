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
            ValidateNull(validationResult, nameof(entity.Skippable), entity.Skippable);
            ValidateNullOrEmpty(validationResult, nameof(entity.Hint), entity.Hint);
            ValidateNull(validationResult, nameof(entity.ModuleId), entity.ModuleId);
            if (!entity.Skippable && entity.Answers.Count == 0)
            {
                validationResult.Errors.Add("A question must be skippable when there no answers provided");
            }


            return validationResult;
        }
    }
}
