using System;
using System.Collections.Generic;
using System.Text;
using PimPamProgrammeur.Repository;

namespace PimPamProgrammeur.Dto.Validator
{
    public class UserRequestDtoValidator : Validator<UserRequestDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;

        public UserRequestDtoValidator(IUserRepository userRepository, IClassroomRepository classroomRepository)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;
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

            if (_classroomRepository.GetClassroom(entity.ClassroomId) == null)
            {
                validationResult.Errors.Add($"Classroom {entity.ClassroomId} not found");
            }

            return validationResult;
        }
    }
}
