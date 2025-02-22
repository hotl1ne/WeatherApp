using Microsoft.AspNetCore.Mvc;
using MyTelegramBot;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("/weather")] 
    
    public class WeatherController : Controller
    {
        private readonly TelegramBotService _telegramBotService;

        public WeatherController(TelegramBotService telegramBotService)
        {
            _telegramBotService = telegramBotService;
        }

        [HttpPost("sendWeatherToUser")]
        public async void SendWeatherToUser([FromBody] long userId)
        {
            await _telegramBotService.SendWeatherToUser(userId);
        }

        [HttpPost("sendWeatherToAll")]
        public async void SendWeatherToAll()
        {
            await _telegramBotService.SendWeatherToAll();
        }
    }
}
