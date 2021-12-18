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

                var dateNow = DateTime.Now.ToShortDateString();

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
                    if (hourlyForecasts[i].Date == dateNow)
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
                        dateNow = hourlyForecasts[i].Date;

                        WeatherForecast weatherforecast = null;

                        if (i > 0)
                        {
                            weatherforecast = this._weatherForecastFactory
                            .WithCity(hourlyForecasts[i].City)
                            .WithDate(hourlyForecasts[i - 1].Date)
                            .WithTemperature(Math.Ceiling(temp / tempCount))
                            .WithHumidity(Math.Ceiling(humidity / humidityCount))
                            .WithWindSpeed(Math.Ceiling(windSpeed / windSpeedCount))
                            .WithWeatherDescription(ModifyWeatherDescription(hourlyForecasts[i - 1].WeatherDescription))
                            .Build();
                        }

                        else
                        {
                            weatherforecast = this._weatherForecastFactory
                            .WithCity(hourlyForecasts[i].City)
                            .WithDate(hourlyForecasts[i].Date)
                            .WithTemperature(Math.Ceiling(hourlyForecasts[i].Temperature))
                            .WithHumidity(Math.Ceiling(hourlyForecasts[i].Humidity))
                            .WithWindSpeed(Math.Ceiling(hourlyForecasts[i].WindSpeed))
                            .WithWeatherDescription(ModifyWeatherDescription(hourlyForecasts[i].WeatherDescription))
                            .Build();
                        }

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
                        .WithDate(hourlyForecasts[hourlyForecasts.Count - 1].Date)
                        .WithTemperature(Math.Ceiling(temp / tempCount))
                        .WithHumidity(Math.Ceiling(humidity / humidityCount))
                        .WithWindSpeed(Math.Ceiling(windSpeed / windSpeedCount))
                        .WithWeatherDescription(ModifyWeatherDescription(hourlyForecasts[hourlyForecasts.Count - 1].WeatherDescription))
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

        private string ModifyWeatherDescription(string description)
        {
            var modifiedDescription = string.Empty;

            if (description.Contains("cloudy") || description.Contains("clouds"))
            {
                modifiedDescription = "cloudy";
            }

            else if (description.Contains("rain") || description.Contains("rainy"))
            {
                modifiedDescription = "rainy";
            }
            else if (description.Contains("sun") || description.Contains("sunny") || description.Contains("clear"))
            {
                modifiedDescription = "sunny";
            }

            else if (description.Contains("snow") || description.Contains("snowy"))
            {
                modifiedDescription = "snowy";
            }

            return modifiedDescription;
        }
    }
}
