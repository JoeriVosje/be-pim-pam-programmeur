using System;
using System.Linq;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class ComponentOrderRequestDtoValidator : Validator<ComponentOrderRequestDto>
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentOrderRequestDtoValidator(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public override ValidationResult Validate(ComponentOrderRequestDto entity)
        {
            var validationResult = new ValidationResult();

            var components = _componentRepository.GetComponents().Select(e=> e.Id).ToHashSet();
            if (entity.ComponentIds.Any(e => !components.Contains(e)))
            {
                var missingComponents = entity.ComponentIds.Where(e => !components.Contains(e));
                foreach (var missingComponent in missingComponents)
                {
                    validationResult.Errors.Add($"Component with id {missingComponent} is not found!");
                }
                
            }

            return validationResult;
        }
    }
}
