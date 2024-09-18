using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid APIKey { get; set; }
    }
}
