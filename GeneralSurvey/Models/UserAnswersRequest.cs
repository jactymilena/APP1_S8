using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GeneralSurvey.Models
{
    [ExcludeFromCodeCoverage]
    public class UserAnswersRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int SurveyId { get; set; }
    }
}
