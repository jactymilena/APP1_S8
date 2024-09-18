using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Models;
using GeneralSurvey.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace XUnit.Coverlet.Collector.Tests
{
    [ExcludeFromCodeCoverage]
    public class SurveyController_Post
    {
        [Fact]
        public void SurveyController_Post_Valid()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            var controller = new SurveyController(mockSurveyService.Object);

            var context = new DefaultHttpContext();
            context.Items["User"] = new User { Id = 1 }; // ajout d'un user

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context   // donne le context au controller
            };

            var answers = new List<QuestionAnswer>();
            var answer = new QuestionAnswer
            {
                QuestionId = 1,
                ChoiceId = 1,
                Text = "",
            };
            answers.Add(answer);

            var surveyResponse = new SurveyResponse
            {
                SurveyId = 1,
                UserId = 1,
                QuestionAnswers = answers,
            };

            mockSurveyService.Setup(s => s.RespondToSurvey(surveyResponse)).Returns(true);

            // Act
            var result = controller.Post(surveyResponse);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SurveyController_Post_UserIsNull()
        {
            // Arrange
            var surveyResponse = new SurveyResponse();
            var context = new DefaultHttpContext(); // context sans user
            var mockSurveyService = new Mock<ISurveyService>();
            var controller = new SurveyController(mockSurveyService.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context   // donne le context au controller
            };

            // Act
            var result = controller.Post(surveyResponse);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void SurveyController_Post_NoAnswers()
        {
            // Arrange
            var surveyResponse = new SurveyResponse();
            var mockSurveyService = new Mock<ISurveyService>();
            var controller = new SurveyController(mockSurveyService.Object);

            var context = new DefaultHttpContext();
            context.Items["User"] = new User { Id = 1 }; // ajout d'un user

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context   // donne le context au controller
            };

            // Act
            var result = controller.Post(surveyResponse);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void SurveyController_Post_AnswerIsNull()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            var controller = new SurveyController(mockSurveyService.Object);

            var context = new DefaultHttpContext();
            context.Items["User"] = new User { Id = 1 }; // ajout d'un user

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context   // donne le context au controller
            };

            var surveyResponse = new SurveyResponse
            {
                SurveyId = 1,
                UserId = 1,
                QuestionAnswers = null,
            };

            // Act
            var result = controller.Post(surveyResponse);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void SurveyController_Post_UserAlreadyAnswered()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            var controller = new SurveyController(mockSurveyService.Object);

            var context = new DefaultHttpContext();
            context.Items["User"] = new User { Id = 1 }; // ajout d'un user

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context   // donne le context au controller
            };

            var answers = new List<QuestionAnswer>();
            var answer = new QuestionAnswer
            {
                QuestionId = 1,
                ChoiceId = 1,
                Text = "",
            };
            answers.Add(answer);

            var surveyResponse = new SurveyResponse
            {
                SurveyId = 1,
                UserId = 1,
                QuestionAnswers = answers,
            };

            mockSurveyService.Setup(s => s.RespondToSurvey(surveyResponse)).Returns(false);

            // Act
            var result = controller.Post(surveyResponse);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
