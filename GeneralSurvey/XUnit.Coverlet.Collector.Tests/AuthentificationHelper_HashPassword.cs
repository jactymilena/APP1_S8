using GeneralSurvey.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace XUnit.Coverlet.Collector.Tests
{
    [ExcludeFromCodeCoverage]
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
            // Arrange
            var hasher = new AuthentificationHelper();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => hasher.HashPassword(null, out var _));
            Assert.Equal("password", exception.ParamName);
        }
        [Fact]
        public void AuthentificationHelper_HashPassword_SaltIsEmpty()
        {
            // Arrange
            var password = "password";
            var salt = Array.Empty<byte>();
            var hasher = new AuthentificationHelper();

            // Act & Assert
            var salt_out = hasher.HashPassword(password, out salt);
            Assert.NotNull(salt_out);
            Assert.NotEmpty(salt_out);
        }
        [Fact]
        public void AuthentificationHelper_HashPassword_PwdIsVeryLong()
        {
            // Arrange
            var password = new string('a', 1000);
            var hasher = new AuthentificationHelper();

            // Act & Assert
            var salt = hasher.HashPassword(password, out var _);
            Assert.NotNull(salt);
            Assert.NotEmpty(salt);
        }
    }
}