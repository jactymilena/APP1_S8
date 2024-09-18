using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class ApiKey
    {
        public Guid Key { get; set; }
        public User? User { get; set; }
    }
}
