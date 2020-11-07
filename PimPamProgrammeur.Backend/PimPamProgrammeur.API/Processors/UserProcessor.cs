﻿
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
        private readonly IHashingService _hashingService;
        private readonly IPasswordGeneratorService _passwordGeneratorService;

        public UserProcessor(IUserRepository userRepository, ITokenProvider tokenProvider, IMapper mapper, ISmtpService smtpService, IHashingService hashingService, IPasswordGeneratorService passwordGeneratorService)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
            _smtpService = smtpService;
            _hashingService = hashingService;
            _passwordGeneratorService = passwordGeneratorService;
        }

        public async Task<UserResponseDto> SaveUser(UserRequestDto userRequest)
        {
            var password = _passwordGeneratorService.Generate(8);
            var hashedPassword = _hashingService.HashPassword(password);
            var user = _mapper.Map<User>((hashedPassword, userRequest));

            var savedUser = await _userRepository.SaveUser(user);

            var userResponse = _mapper.Map<UserResponseDto>(savedUser);

            try
            {
                _smtpService.SendEmail(password, userResponse);
            }
            catch
            {
                await _userRepository.DeleteUser(userResponse.Id);
                throw;
            }
            
            return userResponse;
        }

        public async Task DeleteUser(Guid id)
        {
            if (_userRepository.GetUser(id) != null)
            {
                await _userRepository.DeleteUser(id);
            }
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
            var foundUser = _userRepository.FindUser(userLoginRequestDto.Email);

            if (foundUser == null)
            {
                return null;
            }

            if (!_hashingService.VerifyHashedPassword(foundUser.Password, userLoginRequestDto.Password))
            {
                // Maybe return a different response here?
                return null;
            }

            var userResponse = _mapper.Map<UserResponseDto>(foundUser);

            return _tokenProvider.GenerateToken(userResponse);
        }
    }
}
