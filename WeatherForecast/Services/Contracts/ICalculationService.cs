using System.Collections.Generic;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services.Contracts
{
    public interface ICalculationService
    {
        public List<WeatherForecastDto> CalculateAverageMetrics(List<WeatherForecast> hourlyForecasts);
    }
}
