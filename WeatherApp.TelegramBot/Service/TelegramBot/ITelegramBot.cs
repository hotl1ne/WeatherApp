using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.TelegramBot.Service.TelegramBot
{
    public interface ITelegramBot
    {
        void StartBot();
        void StopBot();
        Task SendWeatherToUser(long userId);
        Task SendWeatherToAll();
    }
}
