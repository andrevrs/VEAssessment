using System;
using VE.OneTimePassword.Generator;

namespace VE.OneTimePassword.UI
{
    public class OtpControler
    {
        private IOtpGenerator otpGen = new OtpGenerator();

        public OtpControler() { }

        public bool ValidatePassword(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || login.Length < 4)
            {
                throw new ArgumentException("Login must be at least 4 characters long.");
            }
            if (String.IsNullOrEmpty(password) || password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            var verificationPassword = otpGen.GeneratePassword(login);
            var result = password.Equals(verificationPassword);

            return result;
        }

        public String CreatePassword(String login)
        {
            if (String.IsNullOrEmpty(login) || login.Length < 4)
            {
                throw new ArgumentException("Login must be at least 4 characters long.");
            }
            return otpGen.GeneratePassword(login);
        }
    }
}
 