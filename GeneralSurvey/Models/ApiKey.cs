namespace GeneralSurvey.Models
{
    public class ApiKey
    {
        public Guid Key { get; set; }
        public User? User { get; set; }
    }
}
