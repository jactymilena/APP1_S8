using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class Choice
    {
        public int Id { get; set; }
        public string? Letter { get; set; }
        public string? Response { get; set; }
    }
}
