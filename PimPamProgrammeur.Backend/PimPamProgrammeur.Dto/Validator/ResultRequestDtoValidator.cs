using System;
using System.Collections.Generic;
using System.Text;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ResultRequestDtoValidator : Validator<ResultRequestDto>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        public ResultRequestDtoValidator(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        public override ValidationResult Validate(ResultRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNullOrEmpty(validationResult, nameof(entity.SessionId), entity.SessionId.ToString());
            ValidateNull(validationResult, nameof(entity.StartTime), entity.StartTime);

            var session = this._sessionRepository.GetSession(entity.SessionId);
            if (session == null || session.EndTime != null)
            {
                validationResult.Errors.Add($"Session with id: '{entity.SessionId}' is not open");
            }

            if (!entity.UserId.HasValue)
            {
                validationResult.Errors.Add($"userId doesn't have a value and cannot be retrieved from token");
            }

            return validationResult;
        }
    }
}
