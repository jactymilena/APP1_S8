using Xunit;
using Moq;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using GeneralSurvey.Database;
using GeneralSurvey.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Prime.UnitTests.Services
{
    public class AuthentificationHelper_HashPassword
    {
        [Fact]
        public void AuthentificationHelper_HashPassword_WhenValidArg()
        {
            // Arrange
            var password = "securePassword";
            var hasher = new AuthentificationHelper();

            // Act
            var salt = hasher.HashPassword(password, out var _);

            // Assert
            Assert.NotNull(salt);
            Assert.NotEmpty(salt);
        }
        [Fact]
        public void AuthentificationHelper_HashPassword_PwdIsEmpty()
        {
            // Arrange
            var emptyPwd = "";
            var hasher = new AuthentificationHelper();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => hasher.HashPassword(emptyPwd, out var _));
            Assert.Equal("The password string must not be empty", exception.Message);
        }
        [Fact]
        public void AuthentificationHelper_HashPassword_PwdIsNull()
        {

        }
        [Fact]
        public void AuthentificationHelper_HashPassword_SaltIsEmpty()
        {

        }
        [Fact]
        public void AuthentificationHelper_HashPassword_SaltIsNull()
        {

        }
        [Fact]
        public void AuthentificationHelper_HashPassword_PwdIsTooLong()
        {

        }
    }
}