using PimPamProgrammeur.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public class SmtpService : ISmtpService
    {

        public SmtpService()
        {

        }

        public void SendEmail(string password, UserResponseDto responseDto)
        {
           SendEmail()
            
        }

        private void SendMessage(MailMessage message)
        {
            message.From = new MailAddress(Constants.Smtp.MailAddress, Constants.Smtp.MailSenderAlias);
            message.IsBodyHtml = false;
            using (SmtpClient smtp = new SmtpClient(Constants.Smtp.MailServerAddress, Constants.Smtp.MailServerPort))
            {
                client.Credentials = new NetworkCredential(Constants.Smtp.MailUsername, Constants.Smtp.MailPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = Constants.Smtp.UseSsl;
                client.Send(message);
            }
        }
    }
}
