using Moq;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using GeneralSurvey.Database;
using GeneralSurvey.Helpers;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace XUnit.Coverlet.Collector.Tests
{
    [ExcludeFromCodeCoverage]
    public class UserService_Register
    {
        [Fact]
        public void Register_Successful()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(x => x.VerifyAPIKey(It.IsAny<Guid>())).Returns(true);
            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(new List<User>());
            _mockDbHelper.Setup(x => x.PostUser(It.IsAny<User>()));
            _mockDbHelper.Setup(x => x.PutAPIKey(It.IsAny<int>(), It.IsAny<Guid>()));

            var _mockAuthHelper = new Mock<AuthentificationHelper>();
            _mockAuthHelper
                .Setup(x => x.HashPassword(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
                .Returns(
                    (string password, out byte[] outParam) =>
                    {
                        outParam = System.Text.Encoding.UTF8.GetBytes("salt");
                        return "hash";
                    }
                );

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            RegisterRequest request = new RegisterRequest { APIKey = Guid.NewGuid(), Username = "user", Password = "pass" };

            var response = userService.Register(request);

            Assert.True(response);
        }

        [Fact]
        public void Register_ApiKeyNotAvailableOrDoesNotExist()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(x => x.VerifyAPIKey(It.IsAny<Guid>())).Returns(false);

            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            RegisterRequest request = new RegisterRequest { APIKey = Guid.NewGuid(), Username = "user", Password = "pass" };

            var response = userService.Register(request);

            Assert.False(response);
        }

        [Fact]
        public void Register_UsernameAlreadyExists()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(x => x.VerifyAPIKey(It.IsAny<Guid>())).Returns(true);
            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(new List<User> { new User { Username = "user" } });

            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            RegisterRequest request = new RegisterRequest { APIKey = Guid.NewGuid(), Username = "user", Password = "pass" };

            var response = userService.Register(request);

            Assert.False(response);
        }

        [Fact]
        public void Register_PasswordIsWhitespace()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(x => x.VerifyAPIKey(It.IsAny<Guid>())).Returns(true);

            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            RegisterRequest request = new RegisterRequest { APIKey = Guid.NewGuid(), Username = "user", Password = "" };

            var response = userService.Register(request);

            Assert.False(response);
        }

        [Fact]
        public void Register_PasswordIsNull()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            RegisterRequest registerRequest = new RegisterRequest { APIKey = Guid.NewGuid(), Username = "user", Password = null };

            var response = userService.Register(registerRequest);

            Assert.False(response);
        }

        [Fact]
        public void Register_RequestIsNull()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);

            var response = userService.Register(null);

            Assert.False(response);
        }
    }
}