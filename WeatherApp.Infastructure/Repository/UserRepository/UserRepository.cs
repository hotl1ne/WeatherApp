using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WeatherApp.Infastructure.Models;

namespace WeatherApp.Infastructure.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> AddUser(long telegramId, string favoriteCity)
        {
            const string query = @"
                INSERT INTO Users (Id, TelegramId, FavoriteCity, CreatedAt)
                VALUES (@Id, @TelegramId, @FavoriteCity, @CreatedAt);
            ";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, new
            {
                Id = Guid.NewGuid(),
                TelegramId = telegramId,
                FavoriteCity = favoriteCity,
                CreatedAt = DateTime.UtcNow
            });

            if (result > 0)
            {
                Console.WriteLine($"User added with TelegramId {telegramId}");
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
            return result > 0;
        }

        public async Task<User> GetUserByTelegramId(long telegramId)
        {
            const string query = "SELECT * FROM Users WHERE TelegramId = @TelegramId";

            using var connection = new SqlConnection(_connectionString);
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { TelegramId = telegramId });

            if (user == null)
            {
                Console.WriteLine($"User with TelegramId {telegramId} not found.");
                return null; 
            }

            return user; 
        }

        public async Task<List<User>> GetAllUsers()
        {
            const string query = "SELECT * FROM Users";

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var users = (await connection.QueryAsync<User>(query)).ToList();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка отримання користувачів: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<bool> UpdateUserCity(long telegramId, string favoriteCity)
        {
            const string query = @"
                                UPDATE Users
                                SET FavoriteCity = @FavoriteCity
                                WHERE TelegramId = @TelegramId;";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(query, new { TelegramId = telegramId, FavoriteCity = favoriteCity });

            return result > 0;
        }

        public async Task<UserWithHistory> GetUserWithHistoryByTelegramId(long telegramId)
        {
            const string query = @"
                    SELECT u.Id, u.TelegramId, u.FavoriteCity, u.CreatedAt
                    FROM Users u
                    WHERE u.TelegramId = @TelegramId;

                    SELECT w.HistoryID, w.UserID, w.RequestDate, w.City
                    FROM WeatherHistory w
                    WHERE w.UserID = @TelegramId;";

            using var connection = new SqlConnection(_connectionString);

            var multi = await connection.QueryMultipleAsync(query, new { TelegramId = telegramId });

            var user = await multi.ReadFirstOrDefaultAsync<User>();
            var weatherHistory = (await multi.ReadAsync<WeatherHistory>()).ToList();

            if (user == null)
            {
                return null;
            }

            var userWithHistory = new UserWithHistory
            {
                Id = user.Id,
                TelegramId = user.TelegramId,
                FavoriteCity = user.FavoriteCity,
                CreatedAt = user.CreatedAt, 
                WeatherHistory = weatherHistory 
            };

            return userWithHistory;
        }

    }
}
