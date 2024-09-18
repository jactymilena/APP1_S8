using GeneralSurvey.Models;
using GeneralSurvey.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace XUnit.Coverlet.Collector.Tests
{
    [ExcludeFromCodeCoverage]
    public class AuthentificationHelper_GenerateJwtToken
    {
        [Fact]
        public void AuthentificationHelper_GenerateJwtToken_WhenValidArg()
        {
            // Arrange
            var authHelper = new AuthentificationHelper();
            var user = new User { Id = 1 };
            var secret = "superSecretKey123456789123456789";

            // Act
            var token = authHelper.GenerateJwtToken(user, secret);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public void AuthentificationHelper_GenerateJwtToken_WhenSmallSecret()
        {
            // Arrange
            var authHelper = new AuthentificationHelper();
            var user = new User { Id = 1 };
            var secret = "TooSmallStr";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => authHelper.GenerateJwtToken(user, secret));
            Assert.Equal("The secret string must be longer than 256 bits.", exception.Message);
        }

        [Fact]
        public void AuthentificationHelper_GenerateJwtToken_WhenSecretIsNull()
        {
            // Arrange
            var authHelper = new AuthentificationHelper();
            var user = new User { Id = 1 };

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => authHelper.GenerateJwtToken(user, null));
            Assert.Equal("secret", exception.ParamName);
        }

        [Fact]
        public void AuthentificationHelper_GenerateJwtToken_WhenUserIsNull()
        {
            // Arrange
            var authHelper = new AuthentificationHelper();
            var secret = "superSecretKey123456789123456789";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => authHelper.GenerateJwtToken(null, secret));
            Assert.Equal("user", exception.ParamName);
        }
    }
}