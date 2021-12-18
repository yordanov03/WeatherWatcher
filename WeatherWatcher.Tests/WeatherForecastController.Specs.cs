using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using WeatherWatcher.Api.Models;
using WeatherWatcher.Controllers;
using WeatherWatcher.Services.Contracts;
using Xunit;

namespace WeatherWatcher.Tests
{
    public class WeatherForecastControllerSpecs
    {
        [Fact]
        public void ReturnsForecastByCityName()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);
            var searchKey = new SearchParams { City = "berlin" };

            //Act
            var result = weatherforecastController.GetWeatherForecast(searchKey, CancellationToken.None);

            //Assert
            Assert.NotNull(result.Result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ReturnsForecastByZipcode()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);
            var searchKey = new SearchParams { ZipCode = "12045" };

            //Act
            var result = weatherforecastController.GetWeatherForecast(searchKey, CancellationToken.None);

            //Assert
            Assert.NotNull(result.Result);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ReturnsNothingWhenZipcodeIsInvalid()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);
            var searchKey = new SearchParams { ZipCode = "120456" };

            //Act
            var exception = Record.ExceptionAsync(() => weatherforecastController.GetWeatherForecast(searchKey, CancellationToken.None));

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void ReturnsNothingWhenSearchKeyEmpty()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);

            //Act
            var exception = Record.ExceptionAsync(() => weatherforecastController.GetWeatherForecast(null, CancellationToken.None));

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void ThrowsExceptionWhenCityNameTooShort()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);
            var searchKey = new SearchParams { City = "b" };

            //Act
            var exception = Record.ExceptionAsync(() => weatherforecastController.GetWeatherForecast(searchKey, CancellationToken.None));

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void ThrowsExceptionWhenCityNameTooLong()
        {
            //Arrange
            var forecastService = new Mock<IForecastService>();
            var weatherforecastController = new WeatherForecastController(forecastService.Object);
            var searchKey = new SearchParams { City = "qwertyuiopasdfghjklzxcvbnm" };

            //Act
            var exception = Record.ExceptionAsync(() => weatherforecastController.GetWeatherForecast(searchKey, CancellationToken.None));

            //Assert
            Assert.NotNull(exception);
        }
    }
}
