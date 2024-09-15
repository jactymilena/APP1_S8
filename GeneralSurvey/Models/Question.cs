namespace GeneralSurvey.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public ICollection<Choice>? Choices { get; set; }
    }
}
