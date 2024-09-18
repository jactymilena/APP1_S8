using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class AuthentificationRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
