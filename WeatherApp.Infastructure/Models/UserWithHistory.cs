using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infastructure.Models
{
    public class UserWithHistory
    {
        public Guid Id { get; set; }
        public long TelegramId { get; set; }
        public string FavoriteCity { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<WeatherHistory> WeatherHistory { get; set; } = new List<WeatherHistory>();
    }
}
