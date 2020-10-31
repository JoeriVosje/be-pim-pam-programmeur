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
            MailMessage message = new MailMessage();
            message.To.Add(responseDto.Email);
            message.Subject = $"{Constants.ApplicationName} accountbevestiging";
            message.Body = $"Beste " + responseDto.FirstName + ",\n\n" +
                $"Er is een account voor u aangemaakt op {Constants.ApplicationName}:\n" +
                $"Je kan inloggen met het e-mailadres {responseDto.Email} en het wachtwoord {password} \n" +
                "Klik op de onderstaande link om naar de applicatie te gaan.\n\n" +
                Constants.Authentication.BaseUrl + "\n\n" +
                "Met vriendelijke groeten,\n\n" +
                $"Het {Constants.ApplicationName} team";
            SendMessage(message);
        }

        private void SendMessage(MailMessage message)
        {
            message.From = new MailAddress(Constants.Smtp.MailAddress, Constants.Smtp.MailSenderAlias);
            message.IsBodyHtml = false;
            using (SmtpClient smtp = new SmtpClient(Constants.Smtp.MailServerAddress, Constants.Smtp.MailServerPort))
            {
                _client.Credentials = new NetworkCredential(Constants.Smtp.MailUsername, Constants.Smtp.MailPassword);
                _client.DeliveryMethod = SmtpDeliveryMethod.Network;
                _client.EnableSsl = Constants.Smtp.UseSsl;
                _client.Send(message);
            }
        }
    }
}
