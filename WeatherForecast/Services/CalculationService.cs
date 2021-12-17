using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Factories;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IWeatherForecastFactory _weatherForecastFactory;
        private readonly ILogger<CalculationService> _logger;
        private readonly IMapper _mapper;

        public CalculationService(IWeatherForecastFactory weatherForecastFactory,
            ILogger<CalculationService> logger,
            IMapper mapper)
        {
            this._weatherForecastFactory = weatherForecastFactory;
            this._logger = logger;
            this._mapper = mapper;
        }
        public List<WeatherForecastDto> CalculateAverageMetrics(List<WeatherForecast> hourlyForecasts)
        {
            try
            {
                var dailyForecasts = new List<WeatherForecast>();

                var date = DateTime.Now.Date;

                //a model to be created out of this
                decimal temp = 0;
                decimal humidity = 0;
                decimal windSpeed = 0;

                //counts of properties
                int tempCount = 0;
                int humidityCount = 0;
                int windSpeedCount = 0;


                for (int i = 0; i < hourlyForecasts.Count; i++)
                {
                    if (hourlyForecasts[i].Date.Date == date)
                    {
                        temp += hourlyForecasts[i].Temperature;
                        humidity += hourlyForecasts[i].Humidity;
                        windSpeed += hourlyForecasts[i].WindSpeed;

                        tempCount++;
                        humidityCount++;
                        windSpeedCount++;
                    }

                    else
                    {
                        date = hourlyForecasts[i].Date.Date;

                        var weatherforecast = this._weatherForecastFactory
                            .WithDate(hourlyForecasts[i - 1].Date.Date)
                            .WithTemperature(Math.Ceiling(temp / tempCount))
                            .WithHumidity(Math.Ceiling(humidity / humidityCount))
                            .WithWindSpeed(Math.Ceiling(windSpeed / windSpeedCount))
                            .WithWeatherDescription(hourlyForecasts[i - 1].WeatherDescription)
                            .Build();

                        dailyForecasts.Add(weatherforecast);

                        temp = 0;
                        humidity = 0;
                        windSpeed = 0;

                        tempCount = 0;
                        humidityCount = 0;
                        windSpeedCount = 0;

                        i--;

                    }
                }

                // calculate last day's forecast

                if (tempCount > 0 || humidityCount > 0 || windSpeedCount > 0)
                {

                    var lastWeatherForecast = this._weatherForecastFactory
                        .WithDate(hourlyForecasts[hourlyForecasts.Count - 1].Date.Date)
                        .WithTemperature(Math.Ceiling(temp / tempCount))
                        .WithHumidity(Math.Ceiling(humidity / humidityCount))
                        .WithWindSpeed(Math.Ceiling(windSpeed / windSpeedCount))
                        .WithWeatherDescription(hourlyForecasts[hourlyForecasts.Count - 1].WeatherDescription)
                        .Build();

                    dailyForecasts.Add(lastWeatherForecast);
                }

                this._logger.LogDebug("Daily forecasts comprised successfully");

                try
                {
                    var dailyForecastsDto = this._mapper.Map<List<WeatherForecast>, List<WeatherForecastDto>>(dailyForecasts);

                    this._logger.LogDebug("Mapping done successfully");
                    return dailyForecastsDto;
                }
                catch (Exception)
                {
                    this._logger.LogError("Could not map parsed forecasts");
                    throw new CalculationException("Oops something went wrong with calculation");
                }

            }
            catch
            {
                this._logger.LogError("Could not group forecasts");
                throw new CalculationException("Oops something went wrong with calculation");
            }
        }
    }
}
