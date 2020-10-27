
namespace PimPamProgrammeur.Dto.Validator
{
    public class ModuleRequestDtoValidator : IValidator<ModuleRequestDto>
    {
        public ValidationResult Validate(ModuleRequestDto entity)
        {
            var validationResult = new ValidationResult();
            
            if (string.IsNullOrEmpty(entity.Name))
            {
                validationResult.Errors.Add("Module cannot be empty");
            }

            return validationResult;
        }
    }
}
