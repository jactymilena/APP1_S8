using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface IUserService
    {
        AuthentificationResponse? Authenticate(AuthentificationRequest model);
        bool Register(RegisterRequest model);
        List<User> GetAll();
        User GetById(int id);
        void AddUser(User user);
    }
}
