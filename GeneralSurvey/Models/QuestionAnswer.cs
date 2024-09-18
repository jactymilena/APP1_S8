using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class QuestionAnswer
    {
        [JsonIgnore]
        public int QuestionId { get; set; }
        public int ChoiceId { get; set; }
        [JsonIgnore]
        public string? Text { get; set; }
    }
}
