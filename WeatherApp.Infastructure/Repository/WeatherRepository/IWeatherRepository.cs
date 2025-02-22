using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infastructure.Repository.WeatherRepository
{
    public interface IWeatherRepository
    {
        Task<bool> SaveWeatherHistory(long userId, string city);
    }
}
