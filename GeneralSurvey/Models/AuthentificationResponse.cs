namespace GeneralSurvey.Models
{
    public class AuthentificationResponse(User user, string token)
    {
        public int Id { get; set; } = user.Id;
        public string AccessToken { get; set; } = token;
    }
}
