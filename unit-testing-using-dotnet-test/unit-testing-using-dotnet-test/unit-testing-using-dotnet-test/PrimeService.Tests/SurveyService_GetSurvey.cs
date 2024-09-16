using Xunit;
using GeneralSurvey.Models;
using GeneralSurvey.Services;

namespace Prime.UnitTests.Services
{
    public class SurveyService_GetSurvey
    {
        [Fact]
        public void GetSurvey_ReturnsSurvey_WhenIdIsValid()
        {
            var expectedResult = new Survey { Id = 1, Title = "Sondage 1" };
            var result = surveyService.GetSurvey(1);

            Assert.NotNull(result);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Title, result.Title);
        }
        [Fact]
        public void GetSurvey_ReturnsSurvey_WhenIdIsInvalid()
        {
            var result = surveyService.GetSurvey(-1);
            Assert.Null(result);
        }
    }
}