using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.WeatherService
{
    public interface IWeatherService
    {
        Task<bool> SaveWeatherHistory(long userId, string city);
    }
}
