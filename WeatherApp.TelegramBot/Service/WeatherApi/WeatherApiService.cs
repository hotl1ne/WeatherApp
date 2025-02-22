using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherApp.Shared.DTO.WeatherDTO;

namespace WeatherApp.TelegramBot.Service.WeatherApi
{
    public class WeatherApiService : IWeatherApi
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient = new HttpClient();

        public WeatherApiService(IConfiguration configuration)
        {
            _apiKey = configuration["WeatherApiKey"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<WeatherResponseDTO> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while calling WeatherAPI");
            }

            var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponseDTO>();
            return weatherData ?? throw new Exception("Invalid response from WeatherAPI");
        }
    }
}
