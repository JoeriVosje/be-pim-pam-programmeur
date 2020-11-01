using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public interface IHashingService
    {
        /// <summary>
        /// Hashes a password using the email address as salt and a random generated string as pepper.
        /// </summary>
        /// <param name="email">Email address that is used as salt.</param>
        /// <param name="plainTextPassword">Plaintext password that needs to be hashed</param>
        /// <returns>The RFC 2898 hashed password.</returns>
        string HashPassword(string email, string plainTextPassword);
    }
}
