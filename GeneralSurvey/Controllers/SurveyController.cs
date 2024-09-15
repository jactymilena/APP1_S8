using GeneralSurvey.Database;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
        public IActionResult GetAnswers(int id)
        {
            var answer = _surveyService.GetAllAnswersBySurveyId(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] UserAnswer userAnswer)
        {
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
