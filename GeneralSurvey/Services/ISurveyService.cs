using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface ISurveyService
    {
        Survey? GetSurvey(int id);
        // List<Answer> GetAllAnswer();
        Answer AddFormAnswer(Answer obj);
        // Answer? UpdateFormAnswer(int id, Answer obj);
    }
}
