using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class EmptyResultRequestDtoValidator : Validator<EmptyResultRequestDto>
    {
        private readonly ISessionRepository _sessionRepository;

        public EmptyResultRequestDtoValidator(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public override ValidationResult Validate(EmptyResultRequestDto entity)
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
