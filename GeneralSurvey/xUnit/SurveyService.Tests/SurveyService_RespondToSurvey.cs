using Xunit;
using Moq;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using GeneralSurvey.Database;

namespace Prime.UnitTests.Services
{
    public class SurveyService_RespondToSurvey
    {
        [Fact]
        public void RespondToSurvey_ReturnsTrue_WhenSurveyResponseIsValid()
        {
            var mockDbHelper = new Mock<DataBaseHelper>();
            var surveyResponse = new SurveyResponse { SurveyId = 1, UserId = 1 };
            mockDbHelper.Setup(db => db.PostUserAnswerSurvey(surveyResponse)).Returns(true);

            var surveyService = new SurveyService(mockDbHelper.Object);

            var response = surveyService.RespondToSurvey(surveyResponse);
            
            Assert.True(response);
        }

        [Fact]
        public void RespondToSurvey_ReturnsFalse_UserAlreadyAnswered()
        {
            var mockDbHelper = new Mock<DataBaseHelper>();
            var surveyResponse = new SurveyResponse { SurveyId = 1, UserId = 1 };
            mockDbHelper.Setup(db => db.HasUserAlreadyAnswered(1, 1)).Returns(true);
            var surveyService = new SurveyService(mockDbHelper.Object);

            var response = surveyService.RespondToSurvey(surveyResponse);

            Assert.False(response);
        }

        [Fact]
        public void RespondToSurvey_ReturnsFalse_WhenInvalidChoices()
        {
            var mockDbHelper = new Mock<DataBaseHelper>();
            var surveyResponse = new SurveyResponse { SurveyId = 1, UserId = 1 };
            mockDbHelper.Setup(db => db.ValidateChoices(surveyResponse)).Returns(false);

            var surveyService = new SurveyService(mockDbHelper.Object);

            var response = surveyService.RespondToSurvey(surveyResponse);
            
            Assert.False(response);
        }
    }
}