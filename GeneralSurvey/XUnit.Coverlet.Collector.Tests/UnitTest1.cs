/*using Xunit;
using Moq;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using GeneralSurvey.Database;
using GeneralSurvey.Helpers;
using Microsoft.Extensions.Options;

namespace XUnit.Coverlet.Collector.Tests
{
    public class UserService_Get
    {
        [Fact]
        public void GetById()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            var user = new User { Id = 1, Username = "test", Password = "password", Salt = "salt" };
            _mockDbHelper.Setup(db => db.GetUserByID(1))
                .Returns(user);

            var _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockAppSettings.Setup(x => x.Value).Returns(new AppSettings { Secret = "secret" });

            var _mockAuthentificationHelper = new Mock<AuthentificationHelper>();

            var userService = new UserService(_mockDbHelper.Object, _mockAppSettings.Object, _mockAuthentificationHelper.Object);
            var result = userService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Salt, result.Salt);
        }

        [Fact]
        public void GetById_WhenNotExist()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(db => db.GetUserByID(1))
                .Returns(new User());

            var _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockAppSettings.Setup(x => x.Value).Returns(new AppSettings { Secret = "secret" });

            var _mockAuthentificationHelper = new Mock<AuthentificationHelper>();

            var userService = new UserService(_mockDbHelper.Object, _mockAppSettings.Object, _mockAuthentificationHelper.Object);
            var result = userService.GetById(1);

            Assert.Empty(result.Username);
            Assert.Empty(result.Password);
        }

        [Fact]
        public void GetById_WhenNull()
        {
            var _mockDbHelper = new Mock<DataBaseHelper>();
            _mockDbHelper.Setup(db => db.GetUserByID(1))
                .Returns((User)null);

            var _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockAppSettings.Setup(x => x.Value).Returns(new AppSettings { Secret = "secret" });

            var _mockAuthentificationHelper = new Mock<AuthentificationHelper>();

            var userService = new UserService(_mockDbHelper.Object, _mockAppSettings.Object, _mockAuthentificationHelper.Object);
            var result = userService.GetById(1);

            Assert.Null(result);
        }
    }
}*/