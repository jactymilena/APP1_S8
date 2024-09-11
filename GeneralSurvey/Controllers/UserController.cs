using GeneralSurvey.Helpers;
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthentificationRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var response = await _userService.AddAndUdateUser(user);

            if (response == null)
                return BadRequest(new { message = "Username or email is already taken" });

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest(new { message = "Id is incorrect" });

            var response = await _userService.AddAndUdateUser(user);

            if (response == null)
                return BadRequest(new { message = "Username or email is already taken" });

            return Ok(response);
        }
    }
}
