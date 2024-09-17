using GeneralSurvey.Helpers;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralSurvey.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet("GetSurvey/{id}")]
        [Authorize]
        public IActionResult GetSurvey(int id)
        {
            var survey = _surveyService.GetSurvey(id);
            if (survey == null)
            {
                return NotFound();
            }
            return Ok(survey);
        }

        [HttpGet("GetAnswers/{id}")]
        [Authorize]
        public IActionResult GetAnswers(int id)
        {
            var answer = _surveyService.GetAllAnswersBySurveyId(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost("AnswerSurvey")]
        [Authorize]
        public IActionResult Post([FromBody] SurveyResponse surveyResponse)
        {
            HttpContext context = HttpContext;
            var user = (User?)context.Items["User"];

            if (user == null)
            {
                return BadRequest(new { message = "Invalid token." });
            }

            if (surveyResponse.QuestionAnswers == null || surveyResponse.QuestionAnswers.Count == 0)
            {
                return BadRequest(new { message = "No answers provided." });
            }

            surveyResponse.UserId = user.Id;
            
            if(_surveyService.RespondToSurvey(surveyResponse))
            {
                return Ok(new { message = "User successfully answered to the survey." });
            }

            return BadRequest(new { message = "User already answered to the survey or user entered invalid choices." });
        }
    }
}
