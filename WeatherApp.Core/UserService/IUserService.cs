using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infastructure.Models;

namespace WeatherApp.Core.UserService
{
    public interface IUserService
    {
        Task<bool> AddUser(long telegramId, string city);
        Task<UserWithHistory> GetUserWithHistoryByTelegramId(long telegramId);
        Task<List<User>> GetAllUsers();
    }
}
