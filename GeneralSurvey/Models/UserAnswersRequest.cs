using System.Text.Json.Serialization;

namespace GeneralSurvey.Models
{
    public class UserAnswersRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int SurveyId { get; set; }
    }
}
