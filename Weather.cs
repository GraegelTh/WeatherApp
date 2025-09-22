using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
