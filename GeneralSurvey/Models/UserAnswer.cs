using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class UserAnswer
    {
        public int UserId { get; set; }
        public int SurveyId { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
