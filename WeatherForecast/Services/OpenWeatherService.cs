using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Factories;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IWeatherForecastFactory _weatherForecastFactory;
        private readonly IDeserializeService _deserializeService;
        private readonly ILogger<CalculationService> _logger;

        public OpenWeatherService(IHttpClientFactory httpFactory,
            IWeatherForecastFactory weatherForecastFactory,
            IDeserializeService deserializeService,
            ILogger<CalculationService> logger)
        {
            this._httpFactory = httpFactory;
            this._weatherForecastFactory = weatherForecastFactory;
            this._deserializeService = deserializeService;
            this._logger = logger;
        }
        public async Task<List<WeatherForecast>> GetWeatherForecast(string url, CancellationToken cancellationToken)
        {
            var forecasts = new List<WeatherForecast>();

            // 1. Deserialize the response.
            var openWeatherResponse = await this._deserializeService.DeserializeJson(url, cancellationToken);

            // 2. Build the list of forecasts

            foreach (var forecast in openWeatherResponse.Forecasts)
            {

                var formattedForecast = this._weatherForecastFactory
                    .WithCity(openWeatherResponse.CityInfo.CityName)
                    .WithDate(DateTime.Parse(forecast.Date).ToShortDateString())
                    .WithTemperature(forecast.MainJsonData.Temperature)
                    .WithHumidity(forecast.MainJsonData.Humidity)
                    .WithWindSpeed(forecast.WindData.WindSpeed)
                    .WithWeatherDescription(forecast.Weather[0].WeatherDescription)
                    .Build();

                forecasts.Add(formattedForecast);
            }

            this._logger.LogDebug("Successfully created weatherforecast model out of incoming json data");
            return forecasts;
        }
    }
}
