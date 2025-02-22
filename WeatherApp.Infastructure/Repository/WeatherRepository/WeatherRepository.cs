using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infastructure.Repository.WeatherRepository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _connectionString;

        public WeatherRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> SaveWeatherHistory(long userId, string city)
        {
            const string query = @"
            INSERT INTO WeatherHistory (HistoryID, UserID, RequestDate, City)
            VALUES (@HistoryID, @UserID, @RequestDate, @City);
        ";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, new
            {
                HistoryID = Guid.NewGuid(),            
                UserID = userId,                      
                RequestDate = DateTime.UtcNow,        
                City = city                          
            });

            return result > 0;
        }
    }
}
