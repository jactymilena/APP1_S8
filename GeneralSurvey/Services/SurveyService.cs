using GeneralSurvey.Database;
using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly DataBaseHelper _dataBaseHelper;

        public SurveyService(DataBaseHelper dataBaseHelper)
        {
            _dataBaseHelper = dataBaseHelper;
        }

        public Survey? GetSurvey(int id)
        {
            return _dataBaseHelper.GetSurveyById(id);
        }

        public List<Answer> GetAllAnswersBySurveyId(int id)
        {
            return _dataBaseHelper.GetAnwsersBySurveyId(id);
        }

        public bool RespondToSurvey(SurveyResponse surveyResponse)
        {
            return _dataBaseHelper.PostUserAnswerSurvey(surveyResponse);
        }
    }
}
