using System.Linq;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class OpenSessionRequestDtoValidator : SessionRequestDtoValidator
    {
        private readonly ISessionRepository _repository;
        private readonly IClassroomRepository _classroomRepository;

        public OpenSessionRequestDtoValidator(ISessionRepository repository, IClassroomRepository classroomRepository)
        {
            _repository = repository;
            _classroomRepository = classroomRepository;
        }

        public override ValidationResult Validate(SessionRequestDto entity)
        {
            var validation = base.Validate(entity);
            if (validation.Errors.Any())
            {
                return validation;
            }

            var classroomByModule = _classroomRepository.GetClassroomByModule(entity.ModuleId);
            if (classroomByModule == null)
            {
                validation.Errors.Add($"No classroom was assigned to {entity.ModuleId}");
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
