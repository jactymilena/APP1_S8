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
            // Appeler la méthode GetSurveyById de DataBaseHelper
            return _dataBaseHelper.GetSurveyById(id);
        }

        public void AddAnswers(List<Answer> answers)
        {
            _dataBaseHelper.PostAnswers(answers);
            // is filled à true
        }

        //List<FormResponse> ISurveyService.GetAllFormResponse()
        //{
        //    return _formResponsesList;
        //}

        //FormResponse? IFormService.GetFormById(int id)
        //{
        //    return _formResponsesList.FirstOrDefault(form => form.Id == id);
        //}

        //FormResponse ISurveyService.AddFormResponse(UpdateFormResponse obj)
        //{
        //    var addFormResponse = new FormResponse()
        //    {
        //        Id = _formResponsesList.Max(form => form.Id) + 1,
        //        Response1 = obj.Response1,
        //    };

        //    _formResponsesList.Add(addFormResponse);
        //    return addFormResponse;
        //}

        //public FormResponse? UpdateFormResponse(int id, UpdateFormResponse obj)
        //{
        //    var formResponseIndex = _formResponsesList.FindIndex(index => index.Id == id);

        //    if (formResponseIndex >= 0)
        //    {
        //        var formResponse = _formResponsesList[formResponseIndex];
        //        formResponse.Response1 = obj.Response1;
        //        _formResponsesList[formResponseIndex] = formResponse;
        //        return formResponse;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //bool IFormService.DeleteFormResponseById(int id)
        //{
        //    var formResponseIndex = _formResponsesList.FindIndex(index => index.Id == id);

        //    if (formResponseIndex >= 0)
        //    {
        //        _formResponsesList.RemoveAt(formResponseIndex);
        //    }
        //    return formResponseIndex >= 0;
        //}
    }
}
