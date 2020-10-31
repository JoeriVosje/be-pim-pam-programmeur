
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


        public struct Smtp
        {
            public const string MailServerAddress = "smtp.gmail.com";
            public static readonly int MailServerPort = 587;
            public const string MailUsername = "pimpamprogrammeur@gmail.com";
            public const string MailPassword = "#pE_^Lm64@pRLqGL";
            public static readonly bool UseSsl = true;
            public const string MailAddress = "pimpamprogrammeur@gmail.com";
            public const string MailSenderAlias = Constants.ApplicationName;
        }

        public struct Authentication
        {
            public const string BaseUrl = "https://google.com";
        }
    }
}
