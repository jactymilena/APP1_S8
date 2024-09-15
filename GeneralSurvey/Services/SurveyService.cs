using GeneralSurvey.Database;
using GeneralSurvey.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace GeneralSurvey.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly DataBaseHelper _dataBaseHelper;

        public SurveyService(DataBaseHelper dataBaseHelper)
        {
            _dataBaseHelper = dataBaseHelper;
        }

        public Survey GetSurvey(int id)
        {
            return _dataBaseHelper.GetSurveyById(id);
        }

        public List<Answer> GetAllAnswersBySurveyId(int id)
        {
            return _dataBaseHelper.GetAnwsersBySurveyId(id);
        }

        public bool RespondToSurvey(UserAnswer userAnswer)
        {
            return _dataBaseHelper.PostUserAnswerSurvey(userAnswer);
        }

        public void AddAnswers(List<Answer> answers)
        {
            _dataBaseHelper.PostAnswers(answers);
        }

    }
}
