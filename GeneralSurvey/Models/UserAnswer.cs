namespace GeneralSurvey.Models
{
    public class UserAnswer
    {
        public int IdUser { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
