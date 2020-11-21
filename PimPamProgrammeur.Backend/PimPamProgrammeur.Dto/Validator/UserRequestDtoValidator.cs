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

            if (_userRepository.FindUser(entity.Email) != null)
            {
                validationResult.Errors.Add($"User with email {entity.Email} already added");
            }

            if (!entity.ClassroomId.HasValue && entity.Role == 0)
            {
                validationResult.Errors.Add("A classroomId is required when creating a student");
            }

            if (entity.ClassroomId.HasValue && _classroomRepository.GetClassroom(entity.ClassroomId.Value) == null)
            {
                validationResult.Errors.Add($"Classroom {entity.ClassroomId.Value} not found");
            }

            return validationResult;
        }
    }
}
