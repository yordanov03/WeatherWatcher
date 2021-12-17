using System;

namespace WeatherWatcher.Api.Dtos
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public string WeatherDescription { get; set; }

    }
}
