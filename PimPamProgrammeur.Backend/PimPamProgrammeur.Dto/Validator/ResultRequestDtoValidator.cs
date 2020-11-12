using System;
using System.Collections.Generic;
using System.Text;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ResultRequestDtoValidator : Validator<ResultRequestDto>
    {
        private readonly ISessionRepository sessionRepository;

        public ResultRequestDtoValidator(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }


        public override ValidationResult Validate(ResultRequestDto entity)
        {
            var validationResult = new ValidationResult();

            var session = this.sessionRepository.GetSession(entity.SessionId);
            if (session == null || session.EndTime != DateTime.MinValue)
            {
                validationResult.Errors.Add($"Session with id: '{entity.SessionId}' is not open");
            }

            // ValidateNullOrEmpty(validationResult, nameof(entity.AnswerId), entity.AnswerId.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.SessionId), entity.SessionId.ToString());
            ValidateNullOrEmpty(validationResult, nameof(entity.UserId), entity.UserId.ToString());

            return validationResult;
        }
    }
}
