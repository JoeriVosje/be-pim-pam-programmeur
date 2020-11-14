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
            if (string.IsNullOrEmpty(entity.Question) && string.IsNullOrEmpty(entity.Theory))
            {
                validationResult.Errors.Add("Either question or theory must have a value");
            }

            ValidateNullOrEmpty(validationResult, nameof(entity.Title), entity.Title);
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
