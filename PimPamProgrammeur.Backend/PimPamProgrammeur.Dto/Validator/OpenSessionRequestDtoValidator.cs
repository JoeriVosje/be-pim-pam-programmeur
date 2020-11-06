using System.Linq;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class OpenSessionRequestDtoValidator : SessionRequestDtoValidator
    {
        private readonly ISessionRepository _repository;

        public OpenSessionRequestDtoValidator(ISessionRepository repository)
        {
            this._repository = repository;
        }

        public override ValidationResult Validate(SessionRequestDto entity)
        {
            var validation = base.Validate(entity);
            if (validation.Errors.Any())
            {
                return validation;
            }

            var session = _repository.GetOpenSessions(entity.ModuleId).ToList();
            if (session.Count >= 1)
            {
                validation.Errors.Add($"A session for {entity.ModuleId} was already opened");
            }

            return validation;
        }
    }
}
