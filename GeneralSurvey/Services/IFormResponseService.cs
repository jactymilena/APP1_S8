using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface IFormResponseService
    {
        List<FormResponse> GetAllFormResponse();
        FormResponse? GetFormById(int id);
        FormResponse AddFormResponse(UpdateFormResponse obj);
        FormResponse? UpdateFormResponse(int id, UpdateFormResponse obj);
        bool DeleteFormResponseById(int id);
    }
}
