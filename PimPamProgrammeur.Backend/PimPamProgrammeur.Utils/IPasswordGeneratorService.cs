using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Utils
{
    public interface IPasswordGeneratorService
    {
        /// <summary>
        /// Generates a cryptographically secure random password of a given length.
        /// </summary>
        /// <param name="length">Length of the password that is generated.</param>
        /// <returns>The randomly generated password.</returns>
        string Generate(int length);
    }
}
