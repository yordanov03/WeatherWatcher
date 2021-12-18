using System;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Factories
{
    public interface IWeatherForecastFactory : IFactory<WeatherForecast>
    {
        IWeatherForecastFactory WithCity(string city);
        IWeatherForecastFactory WithDate(string date);
        IWeatherForecastFactory WithTemperature(decimal temperature);
        IWeatherForecastFactory WithHumidity(decimal humidity);
        IWeatherForecastFactory WithWindSpeed(decimal windSpeed);
        IWeatherForecastFactory WithWeatherDescription(string weatherDescription);
    }
}
