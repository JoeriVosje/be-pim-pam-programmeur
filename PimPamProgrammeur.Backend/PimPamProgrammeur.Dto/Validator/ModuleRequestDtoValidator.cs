
namespace PimPamProgrammeur.Dto.Validator
{
    public class ModuleRequestDtoValidator : Validator<ModuleRequestDto>
    {
        public override ValidationResult Validate(ModuleRequestDto entity)
        {
            var validationResult = new ValidationResult();

            ValidateNullOrEmpty(validationResult, nameof(entity.Name), entity.Name);

            return validationResult;
        }
    }
}
