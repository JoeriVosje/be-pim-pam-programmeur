using System;
using System.Collections.Generic;
using System.Text;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class UserRequestDtoValidator : Validator<UserRequestDto>
    {
        private readonly IUserRepository _userRepository;

        public UserRequestDtoValidator(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public override ValidationResult Validate(UserRequestDto entity)
        {
            var validationResult = new ValidationResult();

            this.ValidateNullOrEmpty(validationResult, nameof(entity.Email), entity.Email);
            this.ValidateNullOrEmpty(validationResult, nameof(entity.FirstName), entity.FirstName);
            this.ValidateNullOrEmpty(validationResult, nameof(entity.LastName), entity.LastName);
            this.ValidateNull(validationResult, nameof(entity.ClassroomId), entity.ClassroomId);

            if (_userRepository.FindUser(entity.Email) != null)
            {
                validationResult.Errors.Add($"User with email {entity.Email} already added");
            }

            return validationResult;
        }
    }
}
