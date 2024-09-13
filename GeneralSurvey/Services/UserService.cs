using GeneralSurvey.Models;
using GeneralSurvey.Database;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeneralSurvey.Services
{
    public class UserService : IUserService
    {
        private DataBaseHelper _db;
        private AppSettings _appSettings;

        public UserService(DataBaseHelper db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public AuthentificationResponse? Authenticate(AuthentificationRequest model)
        {
            var users = _db.GetAllUsers();
            if (users != null)
            {
                var user = users.Find(x => x.Username == model.Username && x.Password == model.Password);
                if (user == null) return null;
                var token = GenerateJwtToken(user);
                
                return new AuthentificationResponse(user, token);
            }
            
            return null;
        }

        public List<User> GetAll()
        {
            return _db.GetAllUsers();
        }

        public User GetById(int id)
        {
            return _db.GetUserByID(id);
        }

        public void AddUser(User user)
        {
            _db.PostUser(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
