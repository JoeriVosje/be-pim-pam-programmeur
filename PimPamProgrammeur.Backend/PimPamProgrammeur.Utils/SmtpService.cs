using PimPamProgrammeur.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public class SmtpService : ISmtpService
    {
        private readonly SmtpClient _client;
        public SmtpService(SmtpClient client)
        {
            _client = client;
        }

        public void SendEmail(string password, UserResponseDto responseDto)
        {
            var message = new MailMessage();
            message.To.Add(responseDto.Email);
            message.Subject = $"Je {Constants.ApplicationName} account is aangemaakt";
            message.Body = $"Beste {responseDto.FirstName}" + ",\n\n" +
                $"Er is een account voor je aangemaakt op {Constants.ApplicationName}.\n\n" +
                $"Je kan de volgende gegevens gebruiken om in te loggen: \n" + 
                $"E-mailadres: {responseDto.Email} \n"+
                $"Wachtwoord:  {password} \n\n" +
                $"Ga snel naar {Constants.Authentication.BaseUrl} en start met het leren programmeren!\n\n" +
                "Heel veel succes!\n\n" +
                $"Het {Constants.ApplicationName} team";
            SendMessage(message);
        }

        private void SendMessage(MailMessage message)
        {
            message.From = new MailAddress(Constants.Smtp.MailAddress, Constants.Smtp.MailSenderAlias);
            message.IsBodyHtml = false;
            using (SmtpClient smtp = new SmtpClient(Constants.Smtp.MailServerAddress, Constants.Smtp.MailServerPort))
            {
                smtp.UseDefaultCredentials = false;
                _client.Credentials = new NetworkCredential(Constants.Smtp.MailUsername, Constants.Smtp.MailPassword);
                _client.DeliveryMethod = SmtpDeliveryMethod.Network;
                _client.EnableSsl = Constants.Smtp.UseSsl;
                _client.Send(message);
            }
        }
    }
}
