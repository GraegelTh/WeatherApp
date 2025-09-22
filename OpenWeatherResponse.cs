using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class OpenWeatherResponse
    {
        public string Name { get; set; } = "";
        public Main main { get; set; } = new();           
        public List<Weather> weather { get; set; } = new();

    }
}
