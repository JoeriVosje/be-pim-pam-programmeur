
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using PimPamProgrammeur.Utils;

namespace PimPamProgrammeur.API.Processors
{
    public class UserProcessor : IUserProcessor
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        private readonly ISmtpService _smtpService;

        public UserProcessor(IUserRepository userRepository, ITokenProvider tokenProvider, IMapper mapper, ISmtpService smtpService)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
            _smtpService = smtpService;
        }

        public async Task<UserResponseDto> SaveUser(UserRequestDto userRequest)
        {
            var password = "abc"; // TODO _passwordGenerator.Generate(6);
            // TODO password = _hashService.generateHash(password)
            var user = _mapper.Map<User>((password, userRequest));

            var savedUser = await _userRepository.SaveUser(user);

            var userResponse = _mapper.Map<UserResponseDto>(savedUser);

            _smtpService.SendEmail(password, userResponse);

            return userResponse;
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);
        }

        public IEnumerable<UserResponseDto> GetUsers()
        {
            var users = _userRepository.GetUsers();

            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public UserResponseDto GetUser(Guid id)
        {
            var user = _userRepository.GetUser(id);

            return user == null ? null : _mapper.Map<UserResponseDto>(user);
        }

        public string Login(UserLoginRequestDto userLoginRequestDto)
        {
            var password = userLoginRequestDto.Password; // TODO _hashService.generateHash(password)

            var foundUser = _userRepository.FindUser(userLoginRequestDto.Email, password);

            if (foundUser == null)
            {
                return null;
            }

            var userResponse = _mapper.Map<UserResponseDto>(foundUser);

            return _tokenProvider.GenerateToken(userResponse);
        }
    }
}
