using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class UserLoginRequestDtoValidator : Validator<UserLoginRequestDto>
    {
        public override ValidationResult Validate(UserLoginRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNullOrEmpty(validationResult, nameof(entity.Email), entity.Email);
            ValidateNullOrEmpty(validationResult, nameof(entity.Password), entity.Password);

            return validationResult;
        }
    }
}
