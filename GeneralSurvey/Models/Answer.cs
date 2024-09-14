using GeneralSurvey.Models;

public class Answer
{
    public int Id { get; set; }
    public int? IdChoice { get; set; }
    public int? IdSurvey { get; set; }
    public DateTime? AnswerDate { get; set; }
    //public Choice? Choice { get; set; }
}