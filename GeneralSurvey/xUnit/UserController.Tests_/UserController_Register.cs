using Xunit;
using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Models;
using GeneralSurvey.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Prime.UnitTests.Services
{
    public class UserController_Resgister
    {
        [Fact]
        public void UserController_Register_Valid()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = "test",
                Password = "test",
                APIKey = Guid.NewGuid(),
            };

            mockUserService.Setup(s => s.Register(registerRequest)).Returns(true);

            var result = controller.Register(registerRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully", ((dynamic)okResult.Value).message);
        }

        [Fact]
        public void UserController_Register_Invalid()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = "test",
                Password = "test",
                APIKey = Guid.NewGuid(),
            };

            mockUserService.Setup(s => s.Register(registerRequest)).Returns(false);

            var result = controller.Register(registerRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username or password is already use", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void UserController_Register_NullModel()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);
        
            var result = controller.Register(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Model is null", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void UserController_Register_Empty()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = "",
                Password = "",
                APIKey = Guid.NewGuid(),
            };

            var result = controller.Register(registerRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username or password is empty", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void UserController_Register_PasswordIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = "test",
                Password = null,
                APIKey = Guid.NewGuid(),
            };

            var result = controller.Register(registerRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username or password is empty", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void UserController_Register_UsernameIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = null,
                Password = "test",
                APIKey = Guid.NewGuid(),
            };

            var result = controller.Register(registerRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username or password is empty", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void UserController_Register_APIKeyIsEmpty()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object);

            var registerRequest = new RegisterRequest
            {
                Username = "test",
                Password = "test",
                APIKey = Guid.Empty,
            };

            var result = controller.Register(registerRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ApiKey is empty", ((dynamic)badRequestResult.Value).message);
        }

    }
}
