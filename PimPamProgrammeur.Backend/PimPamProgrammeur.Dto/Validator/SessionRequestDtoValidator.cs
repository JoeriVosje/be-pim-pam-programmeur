using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class SessionRequestDtoValidator : Validator<SessionRequestDto>
    {
        public override ValidationResult Validate(SessionRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNull(validationResult, nameof(entity.ModuleId), entity.ModuleId);

            return validationResult;
        }
    }
}
