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
    public class UserService_Authentificate
    {
        [Fact]
        public void Authenticate_Successful()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var users = new List<User> { new User { Username = "user", Password = "pass" } };
            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(users);

            var _mockAuthHelper = new Mock<AuthentificationHelper>();
            _mockAuthHelper.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);
            _mockAuthHelper.Setup(x => x.GenerateJwtToken(It.IsAny<User>(), It.IsAny<string>())).Returns("token");

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "user", Password = "pass" };

            var response = userService.Authenticate(request);

            Assert.NotNull(response);
            Assert.Equal("token", response.AccessToken);
        }

        [Fact]
        public void Authenticate_WrongCredentials()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var users = new List<User>() { new User { Username = "user", Password = "pass" } };
            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(users);

            var _mockAuthHelper = new Mock<AuthentificationHelper>();
            _mockAuthHelper.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(false);
            _mockAuthHelper.Setup(x => x.GenerateJwtToken(It.IsAny<User>(), It.IsAny<string>())).Returns("token");

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "user", Password = "pass" };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_UsernameDoesNotExist()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(new List<User>());

            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "user", Password = "pass" };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_NullRequest()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);

            var response = userService.Authenticate(null);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_EmptyRequest()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest();

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_EmptyUsername()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "", Password = "pass" };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_EmptyPassword()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "user", Password = "" };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_NullUsername()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = null, Password = "pass" };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_NullPassword()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = "user", Password = null };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_NullUsernameAndPassword()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest { Username = null, Password = null };

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }

        [Fact]
        public void Authenticate_EmptyUsers()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var _mockAuthHelper = new Mock<AuthentificationHelper>();

            _mockDbHelper.Setup(x => x.GetUsersByUsername(It.IsAny<string>())).Returns(new List<User>());

            IOptions<AppSettings> someOptions = Options.Create(new AppSettings { Secret = "secret" });
            var userService = new UserService(_mockDbHelper.Object, someOptions, _mockAuthHelper.Object);
            AuthentificationRequest request = new AuthentificationRequest();

            var response = userService.Authenticate(request);

            Assert.Null(response);
        }
    }
}