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

        //[HttpPost]
        //public IActionResult Post([FromBody] UpdateFormResponse obj)
        //{
        //    var formResponse = _surveyService.AddFormResponse(obj);

        //    if (formResponse == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(new
        //    {
        //        message = "Form Response created!",
        //        id = formResponse!.Id
        //    });
        //}
    }
}
