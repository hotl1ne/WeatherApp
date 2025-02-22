using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infastructure.Models
{
    public class WeatherHistory
    {
        public Guid HistoryID { get; set; } 
        public long UserID { get; set; } 
        public DateTime RequestDate { get; set; } 
        public string City { get; set; } 
    }
}
