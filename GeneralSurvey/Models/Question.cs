using System.Text.Json.Serialization;

namespace GeneralSurvey.Models
{
    public class Question
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string? Title { get; set; }
        public ICollection<Choice>? Choices { get; set; }
    }
}
