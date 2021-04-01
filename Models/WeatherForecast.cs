using System;
using System.Collections.Generic;

namespace WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC => TemperatureK - 273.15;

        public double TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public double TemperatureK { get; set; }
    }
}
