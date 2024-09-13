using GeneralSurvey.Models;

public class SurveyUser
{
    public int Id { get; set; }
    public int IsFilled { get; set; } = 0;
    public int? IdUser { get; set; }
    public int? IdSurvey { get; set; }
    public User? User { get; set; }
    public Survey? Survey { get; set; }
}