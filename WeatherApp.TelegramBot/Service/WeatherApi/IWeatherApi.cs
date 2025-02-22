using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Shared.DTO.WeatherDTO;

namespace WeatherApp.TelegramBot.Service.WeatherApi
{
    public interface IWeatherApi
    {
        Task<WeatherResponseDTO> GetWeatherAsync(string city);
    }
}
