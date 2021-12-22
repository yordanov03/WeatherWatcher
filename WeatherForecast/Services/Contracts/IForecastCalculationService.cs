using System.Collections.Generic;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services.Contracts
{
    public interface IForecastCalculationService
    {
        public List<WeatherForecastDto> CalculateAverageMetrics(List<WeatherForecast> hourlyForecasts);
    }
}
