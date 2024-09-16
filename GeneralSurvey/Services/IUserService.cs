using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface IUserService
    {
        AuthentificationResponse? Authenticate(AuthentificationRequest model);
        bool Register(RegisterRequest model);
        User? GetById(int id);
    }
}
