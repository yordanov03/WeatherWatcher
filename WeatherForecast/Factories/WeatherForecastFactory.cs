using System;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Factories
{
    public class WeatherForecastFactory : IWeatherForecastFactory
    {
        private DateTime date;
        private decimal temperture;
        private decimal humidity;
        private decimal windSpeed;
        private string weatherDescription;

        public WeatherForecast Build()
        {
            return new WeatherForecast(
                date,
                temperture,
                humidity,
                windSpeed,
                weatherDescription);
            
        }

        public IWeatherForecastFactory WithDate(DateTime date)
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
