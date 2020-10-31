using PimPamProgrammeur.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public interface ISmtpService
    {
        void SendEmail(string password, UserResponseDto responseDto);
    }
}
