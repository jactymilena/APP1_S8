using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class Survey
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}
