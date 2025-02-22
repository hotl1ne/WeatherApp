using WeatherApp.Infastructure.Repository.WeatherRepository;

namespace WeatherApp.Core.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherRepository _weatherRepository;

        public WeatherService(WeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;       
        }

        public async Task<bool> SaveWeatherHistory(long userId, string city)
        {
            return await _weatherRepository.SaveWeatherHistory(userId, city);
        }

    }
}
