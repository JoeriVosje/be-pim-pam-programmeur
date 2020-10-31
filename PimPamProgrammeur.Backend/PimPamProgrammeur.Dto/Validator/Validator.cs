using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public abstract class Validator<TDto> : IValidator<TDto> where TDto : class
    {
        public abstract ValidationResult Validate(TDto entity);

        public void ValidateNull<TProp>(ValidationResult result, string propName, TProp value)
        {
            if (value == null)
            {
                result.Errors.Add($"Property: '{propName}' cannot be null");
            }
        }

        public void ValidateNullOrEmpty(ValidationResult result, string propName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                result.Errors.Add($"Property: '{propName}' cannot be null or empty");
            }
        }
    }
}
