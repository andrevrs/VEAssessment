using System;

namespace VE.OneTimePassword.Generator
{
    public interface IOtpGenerator
    {
        /// <summary>
        /// GeneratePassword takes the login as a String and returns a one time password 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        String GeneratePassword(String login);

        /// <summary>
        /// ValidatePassword takes the login and password and returns true if the password is valid or false if not.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidatePassword(String login, String password);
    }
}
