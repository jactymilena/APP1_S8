using GeneralSurvey.Models;
using GeneralSurvey.Database;
using GeneralSurvey.Helpers;
using Microsoft.Extensions.Options;


namespace GeneralSurvey.Services
{
    public class UserService : IUserService
    {
        private DataBaseHelper _db;
        private AppSettings _appSettings;
        private AuthentificationHelper _authentificationHelper;

        public UserService(DataBaseHelper db, IOptions<AppSettings> appSettings, AuthentificationHelper authentificationHelper)
        {
            _db = db;
            _appSettings = appSettings.Value;
            _authentificationHelper = authentificationHelper;
        }

        public AuthentificationResponse? Authenticate(AuthentificationRequest model)
        {
            if (model == null) return null;
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return null;

            var users = _db.GetUsersByUsername(model.Username);
            if (users.Count != 0)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];
                    byte[] salt = Convert.FromHexString(user.Salt);
                    if (_authentificationHelper.VerifyPassword(model.Password, user.Password, salt))
                    {
                        var token = _authentificationHelper.GenerateJwtToken(user, _appSettings.Secret);
                        return new AuthentificationResponse(user, token);
                    }
                }
            }
            
            return null;
        }

       public bool Register(RegisterRequest model)
        {
            if (model == null)
                return false;

            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrEmpty(model.Username))
                return false;

            if (_db.VerifyAPIKey(model.APIKey))
            {
                if (_db.GetUsersByUsername(model.Username).Count != 0)
                    return false;

                var user = new User
                {
                    Username = model.Username,
                    Password = _authentificationHelper.HashPassword(model.Password, out byte[] salt),
                    Salt = Convert.ToHexString(salt)
                };

                _db.PostUser(user);
                _db.PutAPIKey(user.Id, model.APIKey);

                return true;
            }

            return false;
        }

        public virtual User? GetById(int id)
        {
            return _db.GetUserByID(id);
        }
    }
}
