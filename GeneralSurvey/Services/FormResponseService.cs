using GeneralSurvey.Models;
using Microsoft.EntityFrameworkCore;

namespace GeneralSurvey.Services
{
    public class FormResponseService : IFormResponseService
    {
        private readonly List<FormResponse> _formResponsesList;

        public FormResponseService()
        {
            _formResponsesList = new List<FormResponse>()
            {
                new FormResponse()
                {
                    Id = 1,
                }
            };
        }

        List<FormResponse> IFormResponseService.GetAllFormResponse()
        {
            return _formResponsesList;
        }

        FormResponse? IFormResponseService.GetFormById(int id)
        {
            return _formResponsesList.FirstOrDefault(form => form.Id == id);
        }

        FormResponse IFormResponseService.AddFormResponse(UpdateFormResponse obj)
        {
            var addFormResponse = new FormResponse()
            {
                Id = _formResponsesList.Max(form => form.Id) + 1,
                Response1 = obj.Response1,
            };

            _formResponsesList.Add(addFormResponse);
            return addFormResponse;
        }

        public FormResponse? UpdateFormResponse(int id, UpdateFormResponse obj)
        {
            var formResponseIndex = _formResponsesList.FindIndex(index => index.Id == id);

            if (formResponseIndex >= 0)
            {
                var formResponse = _formResponsesList[formResponseIndex];
                formResponse.Response1 = obj.Response1;
                _formResponsesList[formResponseIndex] = formResponse;
                return formResponse;
            }
            else
            {
                return null;
            }
        }

        bool IFormResponseService.DeleteFormResponseById(int id)
        {
            var formResponseIndex = _formResponsesList.FindIndex(index => index.Id == id);

            if (formResponseIndex >= 0)
            {
                _formResponsesList.RemoveAt(formResponseIndex);
            }
            return formResponseIndex >= 0;
        }
    }
}
