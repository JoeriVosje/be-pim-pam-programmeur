
using System.IO;

namespace PimPamProgrammeur.Utils
{
    public static class Constants
    {
        public const string Secret = "WZr6t2fUN^DF5=?yrDWpPSp4f@&Ye+p9hDQ7Xc8+";
        public const string UserId = "UserId";
        public const string RoleId = "RoleId";
        public const string Admin = "Admin";
        public const string Student = "Student";
        public const string TokenAuthenticationScheme = "TokenAuthenticationScheme";
        public const string ApplicationName = "Pim Pam Programmeur";
        public const string AllowedCorsPolicies = "AllowedCorsPolicies";


        public struct Smtp
        {
            public const string MailServerAddress = "smtp.sendgrid.net";
            public static readonly int MailServerPort = 587;
            public const string MailUsername = "apikey";
            public const string MailPassword = "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";
            public static readonly bool UseSsl = true;
            public const string MailAddress = "pimpamprogrammeur@gmail.com";
            public const string MailSenderAlias = Constants.ApplicationName;
        }

        public struct Authentication
        {
            public const string BaseUrl = "https://pimpamprogrammeur.azurewebsites.net/";
        }
    }
}
