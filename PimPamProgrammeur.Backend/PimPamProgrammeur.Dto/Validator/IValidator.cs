using System;

namespace PimPamProgrammeur.Dto.Validator
{
    public interface IValidator<T> where T : class
    {
        ValidationResult Validate(T entity);
    }
}
