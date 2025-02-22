using AutoMapper;
using WeatherApp.Infastructure.Models;
using WeatherApp.Infastructure.Repository.UserRepository;

namespace WeatherApp.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly WeatherService.WeatherService _weatherService;

        public UserService(UserRepository userRepository, WeatherService.WeatherService weatherService)
        {
            _userRepository = userRepository;
            _weatherService = weatherService;
        }

        public async Task<bool> AddUser(long telegramId, string city)
        {
            var user = await _userRepository.GetUserByTelegramId(telegramId);

            if (user == null)
            {
                await _userRepository.AddUser(telegramId, city);
                await _weatherService.SaveWeatherHistory(telegramId, city);
            }
            else
            {
                await _userRepository.UpdateUserCity(telegramId, city);
                await _weatherService.SaveWeatherHistory(telegramId, city);
            }

            return true;
        }

        public async Task<UserWithHistory> GetUserWithHistoryByTelegramId(long telegramId)
        {
            return await _userRepository.GetUserWithHistoryByTelegramId(telegramId);
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository.GetUserByTelegramId(userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
