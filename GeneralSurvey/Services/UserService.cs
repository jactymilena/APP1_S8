using GeneralSurvey.Models;
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
        private readonly SurveyDbContext _db;
        private readonly AppSettings _appSettings;

        public UserService(SurveyDbContext context, IOptions<AppSettings> appSettings)
        {
            _db = context;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthentificationResponse?> Authenticate(AuthentificationRequest model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthentificationResponse(user, token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User?> AddAndUdateUser(User user)
        {
            if (user.Id == 0)
            {
                await _db.Users.AddAsync(user);
            }
            else
            {
                _db.Users.Update(user);
            }

            await _db.SaveChangesAsync();

            return user;
        }

        private string generateJwtToken(User user)
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
