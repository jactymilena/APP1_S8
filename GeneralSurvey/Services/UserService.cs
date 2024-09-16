using GeneralSurvey.Models;
using GeneralSurvey.Database;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace GeneralSurvey.Services
{
    public class UserService : IUserService
    {
        private DataBaseHelper _db;
        private AppSettings _appSettings;

        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserService(DataBaseHelper db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public AuthentificationResponse? Authenticate(AuthentificationRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return null;

            var users = _db.GetUsersByUsername(model.Username);
            if (users != null && users.Count != 0)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];
                    byte[] salt = Convert.FromHexString(user.Salt);
                    if (VerifyPassword(model.Password, user.Password, salt))
                    {
                        var token = GenerateJwtToken(user);
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

            if (_db.VerifyAPIKey(model.APIKey))
            {
                if (string.IsNullOrWhiteSpace(model.Password))
                    return false;

                if (_db.GetUsersByUsername(model.Username).Count != 0)
                    return false;

                var user = new User
                {
                    Username = model.Username,
                    Password = HashPassword(model.Password, out byte[] salt),
                    Salt = Convert.ToHexString(salt)
                };

                AddUser(user);

                _db.PutAPIKey(user.Id, model.APIKey);

                return true;
            }

            return false;
           
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

        private string HashPassword(string password, out byte[] salt)
        {

            salt = RandomNumberGenerator.GetBytes(keySize);
            return HashPassword(password, salt);
        }

        private string HashPassword(string password, byte[] salt)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
