using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface ISurveyService
    {
        Survey? GetSurvey(int id);
        List<Answer> GetAllAnswersBySurveyId(int id);
        bool RespondToSurvey(SurveyResponse surveyResponse);
    }
}
