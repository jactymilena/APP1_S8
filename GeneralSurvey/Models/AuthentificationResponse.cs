using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class AuthentificationResponse(User user, string token)
    {
        public int Id { get; set; } = user.Id;
        public string AccessToken { get; set; } = token;
    }
}
