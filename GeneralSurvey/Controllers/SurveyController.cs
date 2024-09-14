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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var survey = _surveyService.GetSurvey(id);
            if (survey == null)
            {
                return NotFound();
            }
            return Ok(survey);
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] List<Answer> answers)
        {
            if (answers == null || !answers.Any())
            {
                return BadRequest("No answers provided.");
            }
            _surveyService.AddAnswers(answers);
            return Ok();
        }
    }
}
