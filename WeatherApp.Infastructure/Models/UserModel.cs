using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infastructure.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long TelegramId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FavoriteCity { get; set; }
    }
}
