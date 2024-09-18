using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Models;
using GeneralSurvey.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace XUnit.Coverlet.Collector.Tests
{
    [ExcludeFromCodeCoverage]
    public class SurveyController_GetSurvey
    {
        [Fact]
        public void SurveyController_GetSurvey_WhenIdIsValid()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            var expectedSurvey = new Survey { Id = 1, Title = "Sondage 1" };
            mockSurveyService.Setup(s => s.GetSurvey(1)).Returns(expectedSurvey);

            var controller = new SurveyController(mockSurveyService.Object);

            // Act
            var result = controller.GetSurvey(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedSurvey, result.Value);
        }
        [Fact]
        public void SurveyController_GetSurvey_WhenIdIsInvalid()
        {
            // Arrange
            var mockSurveyService = new Mock<ISurveyService>();
            mockSurveyService.Setup(s => s.GetSurvey(It.IsAny<int>())).Returns((Survey)null);

            var controller = new SurveyController(mockSurveyService.Object);

            // Act
            var result = controller.GetSurvey(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
