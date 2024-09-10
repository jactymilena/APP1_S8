using GeneralSurvey.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace GeneralSurvey.Controllers
{
    [Route("api/[controller]")] // api/Form
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormResponseService _formResponseService;
        public FormController(IFormResponseService formResponseService)
        {
            _formResponseService = formResponseService;
        }
    }
}
