using Xunit;
using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Models;
using GeneralSurvey.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Prime.UnitTests.Services
{
    public class UserController_Authentificate
    {
        [Fact]
        public void UserController_Authentificate_WhenModelIsValid()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "test",
                Password = "test",
            };

            var expectedResponse = new AuthentificationResponse(new User { Id = 1 }, "token");

            mockUserService.Setup(s => s.Authenticate(authentificationRequest)).Returns(expectedResponse);

            var result = controller.Authenticate(authentificationRequest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public void UserController_Authentificate_WhenModelIsInvalid()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "test",
                Password = "test",
            };

            mockUserService.Setup(s => s.Authenticate(authentificationRequest)).Returns((AuthentificationResponse)null);

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenModelIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var result = controller.Authenticate(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenModelIsEmpty()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "",
                Password = "",
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenModelUsernameIsEmpty()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "",
                Password = "test",
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenModelPasswordIsEmpty()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "test",
                Password = "",
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenUsernameIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = null,
                Password = "",
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenPasswordIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = "test",
                Password = null,
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserController_Authentificate_WhenUsernameAndPasswordIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var authentificationRequest = new AuthentificationRequest
            {
                Username = null,
                Password = null,
            };

            var result = controller.Authenticate(authentificationRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
