using WeatherApp.Infastructure.Models;

namespace WeatherApp.Infastructure.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> AddUser(long telegramId, string favoriteCity);
        Task<bool> UpdateUserCity(long telegramId, string favoriteCity);
        Task<User> GetUserByTelegramId(long telegramId);
        Task<UserWithHistory> GetUserWithHistoryByTelegramId(long telegramId);
        Task<List<User>> GetAllUsers();
    }
}
