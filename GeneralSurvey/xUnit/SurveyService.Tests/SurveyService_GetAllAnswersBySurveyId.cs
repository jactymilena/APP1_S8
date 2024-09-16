using Xunit;
using Moq;
using GeneralSurvey.Services;
using GeneralSurvey.Database;

namespace Prime.UnitTests.Services
{
    public class SurveyService_GetAllAnswersBySurveyId
    {
        [Fact]
        public void GetAllAnswersBySurveyId_ReturnsAnswers_WhenIdIsValid()
        {
            // Arrange
            var mockDbHelper = new Mock<DataBaseHelper>();
            var expectedResult = new List<Answer> { new Answer { ChoiceId = 1} };
            
            mockDbHelper.Setup(db => db.GetAnwsersBySurveyId(1)).Returns(expectedResult);
            var surveyService = new SurveyService(mockDbHelper.Object);

            // Act
            var result = surveyService.GetAllAnswersBySurveyId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Count, result.Count);
            Assert.Equal(expectedResult[0].ChoiceId, result[0].ChoiceId);
        }

        [Fact]
        public void GetAllAnswersBySurveyId_ReturnsAnswers_WhenIdIsInvalid()
        {
            // Arrange
            var mockDbHelper = new Mock<DataBaseHelper>();
            mockDbHelper.Setup(db => db.GetAnwsersBySurveyId(1)).Returns((List<Answer>)null);
            var surveyService = new SurveyService(mockDbHelper.Object);

            // Act
            var result = surveyService.GetAllAnswersBySurveyId(-1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllAnswersBySurveyId_ReturnsAnswers_WhenIdIsValidAndNoAnswers()
        {
            // Arrange
            var mockDbHelper = new Mock<DataBaseHelper>();
            mockDbHelper.Setup(db => db.GetAnwsersBySurveyId(1)).Returns(new List<Answer>());
            var surveyService = new SurveyService(mockDbHelper.Object);

            // Act
            var result = surveyService.GetAllAnswersBySurveyId(1);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}