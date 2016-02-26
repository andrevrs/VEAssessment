using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VE.OneTimePassword.Generator;
using VE.OneTimePassword.Utils;
using VE.OneTimePassword.UI;

namespace VE.OneTimePassword.Tests
{
    [TestClass]
    public class OtpGeneratorTests
    {
        IOtpGenerator otp = new OtpGenerator();

        [TestMethod]
        public void CreatePasswordReturnsSixDigitPassword()
        {
            String pass = otp.GeneratePassword("andre");

            //assert
            Assert.AreEqual(pass.Length, 6);
        }

        [TestMethod]
        public void ValidatePasswordReturnFalseForDiferentUser()
        {
            String login1 = "andre";
            String login2 = "paola";

            String password1 = otp.GeneratePassword(login1);
            String password2 = otp.GeneratePassword(login2);

            //act
            bool shouldBeTrue = otp.ValidatePassword("andre", password1);
            bool shouldBeFalse = otp.ValidatePassword("paola", password1);

            //assert
            Assert.IsTrue(shouldBeTrue);
            Assert.IsFalse(shouldBeFalse);
        }

        [TestMethod]
        public void ValidatePasswordReturnTrueForValidPass()
        {
            String login1 = "andre";
            
            String pass = otp.GeneratePassword(login1);
            
            //act
            bool shouldBeTrue = otp.ValidatePassword("andre", pass);

            //assert
            Assert.IsTrue(shouldBeTrue);
        }

        [TestMethod]
        public void ValidatePasswordIsStillValidBeforeInterval()
        {
            //arrange
            String login = "andre";
            TimeUtil.Now = new DateTime(2016, 02, 22, 06, 30, 00);
           
            //act
            String pass = otp.GeneratePassword(login);
            TimeUtil.AddSeconds(29);
            bool passShouldBeValid = otp.ValidatePassword(login, pass);

            TimeUtil.ResetToNow();

            //assert
            Assert.IsTrue(passShouldBeValid);
        }

        [TestMethod]
        public void ValidatePasswordIsExpiredAfterInterval()
        {
            //arrange
            String login = "andre";
            TimeUtil.Now = new DateTime(2016, 02, 22, 06, 30, 00);

            //act
            String pass = otp.GeneratePassword(login);
            TimeUtil.AddSeconds(30);
            bool passShouldBeInvalid = otp.ValidatePassword(login, pass);
            

            TimeUtil.ResetToNow();

            //assert
            Assert.IsFalse(passShouldBeInvalid);
        }
    }

    [TestClass]
    public class OtpControllerTests
    {
        OtpControler otpController = new OtpControler();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePasswordThrowsExceptionLoginLenght()
        {
            String login = "abc";
            String pass = otpController.CreatePassword(login);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePasswordThrowsExceptionPasswordLenght()
        {
            String login = "andre";
            String pass = "5char";

            otpController.ValidatePassword(login, pass);
        }

    }
}
