using Microsoft.AspNetCore.Mvc;

namespace GeneralSurvey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public IEnumerable<User> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new User
            {
                Name = "User" + index,
                Email = "user" + index + "@gmail.com",
                Password = "password" + index
            })
            .ToArray();
        }
    }
}
