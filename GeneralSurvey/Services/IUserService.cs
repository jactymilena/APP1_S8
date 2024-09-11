using GeneralSurvey.Models;

namespace GeneralSurvey.Services
{
    public interface IUserService
    {
        Task<AuthentificationResponse?> Authenticate(AuthentificationRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> AddAndUdateUser(User user);
    }
}
