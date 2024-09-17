using Xunit;
using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Prime.UnitTests.Services
{
    public class SurveyController_GetAnswers
    {
        [Fact]
        public void SurveyController_GetAnswers_WhenIdIsValid()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();

            var expectedAnswers = new List<Answer>();
            var answer = new Answer
            {
                ChoiceId = 0,
            };
            expectedAnswers.Add(answer);
            
            mockSurveyService.Setup(s => s.GetAllAnswersBySurveyId(1)).Returns(expectedAnswers);
            var controller = new SurveyController(mockSurveyService.Object);

            // Act
            var result = controller.GetAnswers(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedAnswers, result.Value);
        }
        [Fact]
        public void SurveyController_GetAnswers_WhenIdIsInvalid()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            mockSurveyService.Setup(s => s.GetAllAnswersBySurveyId(It.IsAny<int>())).Returns((List<Answer>)null);

            var controller = new SurveyController(mockSurveyService.Object);

            // Act
            var result = controller.GetAnswers(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
