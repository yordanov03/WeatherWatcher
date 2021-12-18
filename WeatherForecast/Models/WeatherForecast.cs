using System;

namespace WeatherWatcher.Models
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public string Date { get; }
        public decimal Temperature { get; }
        public decimal Humidity { get; }
        public decimal WindSpeed { get; }
        public string WeatherDescription { get; }

        public WeatherForecast(
            string city,
            string date,
            decimal temperature,
            decimal humidity,
            decimal windSpeed,
            string weatherDescription)
        {
            this.City = city;
            this.Date = date;
            this.Temperature = temperature;
            this.Humidity = humidity;
            this.WindSpeed = windSpeed;
            this.WeatherDescription = weatherDescription;
        }
    }
}
