using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class SurveyResponse
    {
        public int SurveyId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
    }
}
