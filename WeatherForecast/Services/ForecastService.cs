using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Common;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;
using WeatherWatcher.Services.Contracts;
using static WeatherWatcher.Api.Common.Constants;

namespace WeatherWatcher.Services
{
    public class ForecastService : IForecastService
    {
        private readonly OpenWeatherApiOptions _options;
        private readonly IForecastCalculationService _calculationService;
        private readonly IOpenWeatherService _openWeatherService;

        public ForecastService(IOptions<OpenWeatherApiOptions> options,
            IForecastCalculationService calculationService,
            IOpenWeatherService openWeatherService)
        {
            this._options = options.Value;
            this._calculationService = calculationService;
            this._openWeatherService = openWeatherService;
        }
        public async Task<List<WeatherForecastDto>> GetForecastByCityName(string location, 
            CancellationToken cancelationtoken)
        {
            ValidateLocationInput(location);
            string url = BuildOpenWeatherUrlByCityName(location, MeasurementUnit.Metric);

            var forecasts = await this._openWeatherService.GetWeatherForecast(url, cancelationtoken);
            var calculatedForecast = this._calculationService.CalculateAverageMetrics(forecasts);

            return calculatedForecast;
        }

        public async Task<List<WeatherForecastDto>> GetForecastByZipCode(string zipcode, 
            CancellationToken cancelationtoken)
        {
            ValidateZipcodeInput(zipcode);
            string url = BuildOpenWeatherUrlByZipCode(zipcode, MeasurementUnit.Metric);
            var forecasts = await this._openWeatherService.GetWeatherForecast(url, cancelationtoken);
            var calculatedForecast = this._calculationService.CalculateAverageMetrics(forecasts);

            return calculatedForecast;
        }

        private string BuildOpenWeatherUrlByCityName(string location, MeasurementUnit unit = MeasurementUnit.Metric)
        {
            return $"{this._options.ApiUrl}" +
                $"q={location}" +
                $"&units={unit}" +
                $"&appid={_options.ApiKey}";
        }

        private string BuildOpenWeatherUrlByZipCode(string zipCode, MeasurementUnit unit = MeasurementUnit.Metric)
        {
            return $"{this._options.ApiUrl}" +
                $"zip={zipCode}," +
                $"{Constants.CountryCode}" +
                $"&units={unit}" +
                $"&appid={_options.ApiKey}";
        }

        private void ValidateLocationInput(string location)
        {
            Guard.ForStringLength<InvalidParameterInputException>(
                location,
                MinLocationLength,
                MaxLocationLength,
                nameof(location));
        }

        private void ValidateZipcodeInput(string zipcode)
        {
            Guard.ForValidZipcode<InvalidParameterInputException>(
                zipcode,
                nameof(zipcode));
        }
    }
}
