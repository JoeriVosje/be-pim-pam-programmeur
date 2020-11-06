using System.Linq;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class CloseSessionRequestDtoValidator : SessionRequestDtoValidator
    {
        private readonly ISessionRepository _repository;

        public CloseSessionRequestDtoValidator(ISessionRepository repository)
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
            if (session.Count == 0)
            {
                validation.Errors.Add("No open session found!");
            }

            return validation;
        }
    }
}
