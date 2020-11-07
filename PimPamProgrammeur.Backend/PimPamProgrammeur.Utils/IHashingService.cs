using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public interface IHashingService
    {
        /// <summary>
        /// Hashes a password using a RFC 2898 algorithm.
        /// </summary>
        /// <param name="plainTextPassword">Plaintext password that needs to be hashed</param>
        /// <returns>The RFC 2898 hashed password.</returns>
        string HashPassword(string plainTextPassword);

        /// <summary>
        /// Verifies if a hash is cryptographically equal to the hashed plaintext password.
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="plainTextPassword"></param>
        /// <returns></returns>
        bool VerifyHashedPassword(string hashedPassword, string plainTextPassword);
    }
}
