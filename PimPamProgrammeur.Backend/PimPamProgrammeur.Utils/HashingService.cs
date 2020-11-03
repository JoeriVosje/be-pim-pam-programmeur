using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;

namespace PimPamProgrammeur.Utils
{
    public class HashingService : IHashingService
    {
        private const string pepper = @";g!dCzSOX*{xk1kDLt&//K<p,hjJI&\tw%PKcgdv\bLNrD!O8eQCG;l*x%\`Snlv\g$h5_u3Vn ~%0-kEZ[= aPjY & V > u &)`Dmy#BN:<IbHN-cDs]-;tDK:<Jw1>V*+J%";

        public string HashPassword(string plainTextPassword)
        {
            return Crypto.HashPassword(plainTextPassword + pepper);
        }

        public bool VerifyHashedPassword(string hashedPassword, string plainTextPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, plainTextPassword + pepper);
        }
    }
}
