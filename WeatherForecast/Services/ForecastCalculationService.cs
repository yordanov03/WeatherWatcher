using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Factories;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;
using static WeatherWatcher.Api.Common.Constants.WeatherDescriptionConstants;


namespace WeatherWatcher.Api.Services
{
    public class ForecastCalculationService : IForecastCalculationService
    {
        private readonly IWeatherForecastFactory _weatherForecastFactory;
        private readonly ILogger<ForecastCalculationService> _logger;
        private readonly IMapper _mapper;

        public ForecastCalculationService(IWeatherForecastFactory weatherForecastFactory,
            ILogger<ForecastCalculationService> logger,
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

                var datetoday = DateTime.UtcNow.ToShortDateString();

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
                    if (hourlyForecasts[i].Date == datetoday)
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
                        datetoday = hourlyForecasts[i].Date;

                        WeatherForecast weatherforecast = null;

                        //if its not 1st or last forecast
                        if (i > 0 && i< hourlyForecasts.Count-2)
                        {
                            weatherforecast = this._weatherForecastFactory
                            .WithCity(hourlyForecasts[i].City)
                            .WithDate(hourlyForecasts[i-1].Date)
                            .WithTemperature(Math.Ceiling(temp / tempCount))
                            .WithHumidity(Math.Ceiling(humidity / humidityCount))
                            .WithWindSpeed(Math.Ceiling(windSpeed / windSpeedCount))
                            .WithWeatherDescription(ModifyWeatherDescription(hourlyForecasts[i-1].WeatherDescription))
                            .Build();

                            i--;
                        }
                        //When the last and only forecast for the day needs to be created
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
                    }
                }

                // calculate average forecast when there are multipule forecast for the last day

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
                catch (Exception e)
                {
                    this._logger.LogError("Could not map parsed forecasts", e);
                    throw new CalculationException("Oops something went wrong with calculation");
                }

            }
            catch(Exception e)
            {
                this._logger.LogError("Could not group forecasts", e);
                throw new CalculationException("Oops something went wrong with calculation");
            }
        }

        private string ModifyWeatherDescription(string description)
        {
            var modifiedDescription = string.Empty;

            if (description.Contains(Cloudy))
            {
                modifiedDescription = "cloudy";
            }

            else if (description.Contains(Rainy))
            {
                modifiedDescription = "rainy";
            }
            else if (description.Contains(Sunny) || description.Contains(Clear))
            {
                modifiedDescription = "sunny";
            }

            else if (description.Contains(Snowy))
            {
                modifiedDescription = "snowy";
            }

            return modifiedDescription;
        }
    }
}
