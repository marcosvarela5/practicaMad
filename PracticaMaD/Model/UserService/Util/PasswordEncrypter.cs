﻿using System;
using System.Text;
using System.Security.Cryptography;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Util
{

    /// <summary>
    /// Static Class with cryptografic utilities
    /// </summary>
    public static class PasswordEncrypter
    {
        /// <summary>
        /// Method to encrypt with a SHA 256 Algorithm
        /// </summary>
        /// <param name="password">String to encrypt</param>
        /// <returns>Returns a String with the <paramref name="password"/> encrypted
        /// </returns>                       
        public static String Crypt(String password)
        {
            HashAlgorithm hashAlg = new SHA256Managed();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] encryptedPasswordBytes = hashAlg.ComputeHash(passwordBytes);

            String encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

            return encryptedPassword;
        }

        /// <summary>
        /// Determines whether [is clear password correct] [the specified clear password].
        /// </summary>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <returns>
        ///   <c>true</c> if [is clear password correct] [the specified clear password]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsClearPasswordCorrect(String clearPassword,
            String encryptedPassword)
        {
            string encryptedClearPassword = Crypt(clearPassword);

            return encryptedClearPassword.Equals(encryptedPassword);
        }
    }
}