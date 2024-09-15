using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralSurvey.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthentificationRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            var response = _userService.Register(model);

            if (!response)
                return BadRequest(new { message = "Username or password is already use" });

            return Ok(new { message = "User registered successfully" });
        }
    }
}
