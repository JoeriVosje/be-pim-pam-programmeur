using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ResultRequestDtoValidator : Validator<ResultRequestDto>
    {
        public override ValidationResult Validate(ResultRequestDto entity)
        {
            var validationResult = new ValidationResult();

            // ValidateNullOrEmpty(validationResult, nameof(entity.AnswerId), entity.AnswerId.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.QuestionId), entity.QuestionId.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.UserId), entity.UserId.ToString());

            return validationResult;
        }
    }
}
