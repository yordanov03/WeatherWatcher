using System;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Factories
{
    public class WeatherForecastFactory : IWeatherForecastFactory
    {
        private string city;
        private string date;
        private decimal temperture;
        private decimal humidity;
        private decimal windSpeed;
        private string weatherDescription;

        public WeatherForecast Build()
        {
            return new WeatherForecast(
                city,
                date,
                temperture,
                humidity,
                windSpeed,
                weatherDescription);
            
        }

        public IWeatherForecastFactory WithCity(string city)
        {
            this.city = city;
            return this;
        }
        public IWeatherForecastFactory WithDate(string date)
        {
            this.date = date;
            return this;
        }

        public IWeatherForecastFactory WithHumidity(decimal humidity)
        {
            this.humidity = humidity;
            return this;
        }

        public IWeatherForecastFactory WithTemperature(decimal temperature)
        {
            this.temperture = temperature;
            return this;
        }

        public IWeatherForecastFactory WithWeatherDescription(string weatherDescription)
        {
            this.weatherDescription = weatherDescription;
            return this;
        }

        public IWeatherForecastFactory WithWindSpeed(decimal windSpeed)
        {
            this.windSpeed = windSpeed;
            return this;
        }
    }
}
