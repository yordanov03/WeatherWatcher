using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Api.Factories;
using WeatherWatcher.Api.Mapper;
using WeatherWatcher.Api.Services;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;
using Xunit;

namespace WeatherWatcher.Tests.Services
{
    public class CalculationServiceSpecs
    {
        private IForecastCalculationService _calculationService;

        public CalculationServiceSpecs()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            var weatherForecastFactory = new Mock<WeatherForecastFactory>();
            var logger = new Mock<ILogger<ForecastCalculationService>>();
            _calculationService = new ForecastCalculationService(weatherForecastFactory.Object, logger.Object, mapper);
        }


        [Fact]
        public void ServiceReturnsForecast()
        {
            //Arrange
            var forecasts = A.Dummy<List<WeatherForecast>>();

            //Act
            var result = this._calculationService.CalculateAverageMetrics(forecasts);

            //Assert
            Assert.IsType<List<WeatherForecastDto>>(result);
            Assert.NotNull(result);
            Assert.Equal(12, result[0].Temperature);
            Assert.Equal(86, result[0].Humidity);
            Assert.Equal(2, result[0].WindSpeed);
            Assert.Equal("cloudy", result[0].WeatherDescription);

        }

        [Fact]
        public void ServiceReturnsEmptyForecastWhenNoDataPassed()
        {
            //Arrange
            var forecasts = new List<WeatherForecast>();

            //Act
            var result = this._calculationService.CalculateAverageMetrics(forecasts);
            var exception = Record.Exception(() => this._calculationService.CalculateAverageMetrics(forecasts));

            //Assert
            Assert.Empty(result);
            Assert.Null(exception);
        }

    }
}
