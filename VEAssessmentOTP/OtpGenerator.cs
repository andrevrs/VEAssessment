using System;
using System.Text;
using System.Security.Cryptography;
using VE.OneTimePassword.Utils;

namespace VE.OneTimePassword.Generator
{
    public class OtpGenerator : IOtpGenerator
    {
        private string secret = "48656c6c6f576f726c644f664f415448";
        private int interval = 30;
        private DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public OtpGenerator(String secret, DateTime epoch, int interval)
        {
            this.secret = secret;
            this.epoch = epoch;
            this.interval = interval;
        }

        public OtpGenerator() : this("48656c6c6f576f726c644f664f415448", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), 30) { }

        public String GeneratePassword(String login)
        {
            byte[] hash = GenerateHash(login);
            //get six digit password from the hash
            String generatedPassword = (CalculateOffset(hash) % 1000000).ToString().PadLeft(6, '0');
            return generatedPassword;
        }

        public bool ValidatePassword(string login, string password)
        {
            var verificationPassword = GeneratePassword(login);
            var result = password.Equals(verificationPassword);

            return result;
        }

        private static int CalculateOffset(byte[] hash)
        {
            int offset = hash[19] & 0xf;
            int binCode = (hash[offset] & 0x7f) << 24
               | (hash[offset + 1] & 0xff) << 16
               | (hash[offset + 2] & 0xff) << 8
               | (hash[offset + 3] & 0xff);
            return binCode;
        }

        private byte[] GenerateHash(string login)
        {
            return GenerateHMAC(login).ComputeHash(CalculateMovingFactor());
        }

        private HMAC GenerateHMAC(string login)
        {
            var key = Encoding.UTF8.GetBytes(String.Concat(login, secret));
            return new HMACSHA1(key);
        }

        private byte[] CalculateMovingFactor()
        {
            var timeNow = TimeUtil.Now;
            var timeElapsed = timeNow.Subtract(epoch).TotalSeconds;
            int counter = (int)timeElapsed / interval;

            return UTF8Encoding.UTF8.GetBytes(counter.ToString());
        }
    }
}
