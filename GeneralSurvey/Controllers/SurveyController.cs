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
        public IActionResult Post([FromBody] UserAnswer userAnswer)
        {
            HttpContext context = HttpContext;
            var user = (User?)context.Items["User"];

            if (user != null && user.Id != userAnswer.IdUser)
            {
                return BadRequest("User id does not match the token.");
            }


            if (userAnswer == null || !userAnswer.Answers.Any())
            {
                return BadRequest("No answers provided.");
            }
            
            if(_surveyService.RespondToSurvey(userAnswer))
            {
                return Ok();
            }

            return BadRequest("User already answered");
        }
    }
}
