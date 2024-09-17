using Xunit;
using GeneralSurvey.Helpers;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Drawing.Printing;

namespace Prime.UnitTests.Services
{
    public class AuthentificationHelper_VerifyPassword
    {
        [Fact]
        public void AuthentificationHelper_ValidPwd()
        {
            // Arrange
            var keySize = 64;
            var password = "password";
            var salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                keySize);

            var helper = new AuthentificationHelper();

            // Act & Assert
            var result = helper.VerifyPassword(password, Convert.ToHexString(hash), salt);
            Assert.True(result);
        }
        [Fact]
        public void AuthentificationHelper_InvalidPwd()
        {
            // Arrange
            var keySize = 64;
            var password = "password";
            var salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                keySize);

            var helper = new AuthentificationHelper();

            // Act & Assert
            var result = helper.VerifyPassword("wrongpassword", Convert.ToHexString(hash), salt);
            Assert.False(result);
        }
        [Fact]
        public void AuthentificationHelper_SaltIsNull()
        {
            // Arrange
            var keySize = 64;
            var password = "password";
            var salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                keySize);

            var helper = new AuthentificationHelper();

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => helper.VerifyPassword(password, Convert.ToHexString(hash), null));
        }
        [Fact]
        public void AuthentificationHelper_SaltHasWrongSize()
        {
            // Arrange
            var keySize = 64;
            var password = "password";
            var salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                keySize);

            var helper = new AuthentificationHelper();

            // Act & Assert
            var exception = Assert.Throws<Xunit.Sdk.EqualException> (() => helper.VerifyPassword(password, Convert.ToHexString(hash), Array.Empty<byte>()));
        }
    }
}
