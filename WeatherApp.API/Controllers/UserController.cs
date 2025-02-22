using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core.UserService;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUserById/{userId}")]
        public async Task<ActionResult> GetUserById(long userId)
        {
            var user = await _userService.GetUserWithHistoryByTelegramId(userId);

            if (user == null)
            {
                return NotFound("Користувача не знайдено");
            }

            return Ok(user);
        }
    }
}
