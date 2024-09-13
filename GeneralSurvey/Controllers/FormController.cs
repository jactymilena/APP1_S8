using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace GeneralSurvey.Controllers
{
    [Route("[controller]")] // api/Form
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormResponseService _formResponseService;
        public FormController(IFormResponseService formResponseService)
        {
            _formResponseService = formResponseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var formResponse = _formResponseService.GetAllFormResponse();
            return Ok(formResponse);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var formResponse = _formResponseService.GetFormById(id);
            if (formResponse == null)
            {
                return NotFound();
            }
            return Ok(formResponse);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UpdateFormResponse obj)
        {
            var formResponse = _formResponseService.AddFormResponse(obj);

            if (formResponse == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Form Response created!",
                id = formResponse!.Id
            });

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateFormResponse obj)
        {
            var formResponse = _formResponseService.UpdateFormResponse(id, obj);

            if (formResponse == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Form Response updated!",
                id = formResponse!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_formResponseService.DeleteFormResponseById(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Form Response updated!",
                id = id
            });
        }
    }
}
