using Xunit;
using Moq;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using GeneralSurvey.Database;

namespace Prime.UnitTests.Services
{
    public class SurveyService_GetSurvey
    {
        [Fact]
        public void GetSurvey_ReturnsSurvey_WhenIdIsValid()
        {
            // Arrange
            var mockDbHelper = new Mock<DataBaseHelper>();
            var expectedResult = new Survey { Id = 1, Title = "Sondage 1" };
            
            mockDbHelper.Setup(db => db.GetSurveyById(1)).Returns(expectedResult);
            var surveyService = new SurveyService(mockDbHelper.Object);

            // Act
            var result = surveyService.GetSurvey(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Title, result.Title);
        }
       /* [Fact]
        public void GetSurvey_ReturnsSurvey_WhenIdIsInvalid()
        {
            var surveyService = new SurveyService(new GeneralSurvey.Database.DataBaseHelper());
            var result = surveyService.GetSurvey(-1);
            Assert.Null(result);
        }*/
    }
}