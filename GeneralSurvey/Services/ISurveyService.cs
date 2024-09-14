using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface ISurveyService
    {
        Survey? GetSurvey(int id);
        //List<Answer> GetAllAnswerBySurveyId();
        void AddAnswers(List<Answer> answers);
    }
}
