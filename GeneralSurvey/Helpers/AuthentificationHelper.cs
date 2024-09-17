using GeneralSurvey.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace GeneralSurvey.Helpers
{
    public class AuthentificationHelper
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public virtual string GenerateJwtToken(User user, string secret)
        {
            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            int lengthInBits = secret.Length * 16;
            if (lengthInBits <= 256)
            {
                throw new ArgumentException("The secret string must be longer than 256 bits.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public virtual string HashPassword(string password, out byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The password string must not be empty");
            }
            salt = RandomNumberGenerator.GetBytes(keySize);
            return HashPassword(password, salt);
        }

        private string HashPassword(string password, byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The password string must not be empty");
            }
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public virtual bool VerifyPassword(string password, string hash, byte[] salt)
        {
            Assert.Equal(keySize, salt.Length);

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
