using System.Linq;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class OpenSessionRequestDtoValidator : SessionRequestDtoValidator
    {
        private readonly ISessionRepository _repository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IComponentRepository _componentRepository;

        public OpenSessionRequestDtoValidator(ISessionRepository repository, IClassroomRepository classroomRepository, IComponentRepository componentRepository)
        {
            _repository = repository;
            _classroomRepository = classroomRepository;
            _componentRepository = componentRepository;
        }

        public override ValidationResult Validate(SessionRequestDto entity)
        {
            var validation = base.Validate(entity);
            if (validation.Errors.Any())
            {
                return validation;
            }

            var hasComponent = _componentRepository.GetComponentsByModule(entity.ModuleId);
            if (!hasComponent.Any())
            {
                validation.Errors.Add("You can't open a session without any components");
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
