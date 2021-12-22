using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using WeatherWatcher.Api.Services;
using WeatherWatcher.Models;
using WeatherWatcher.Tests.Common;
using Xunit;

namespace WeatherWatcher.Tests.Services
{
    public class DeserializeServiceSpecs
    {
        [Fact]
        public void BuildOkResponse()
        {
            //Arrange
            string url = "https://api.openweathermap.org/data/2.5/forecast?q=qwertyz&units=metric&appid=0000";
            var httpFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.OkResponse);
            var logger = new Mock<ILogger<WeatherDataProviderService>>();
            var deserializeService = new WeatherDataProviderService(httpFactory, logger.Object);
            //Act
            var result = deserializeService.ParseWeatherData(url, CancellationToken.None);

            //Assert
            Assert.IsType<OpenWeatherResponse>(result.Result);
        }

        [Fact]
        public void ServiceReturnsExceptionWhenCityNotValid()
        {
            string url = "https://api.openweathermap.org/data/2.5/forecast?q=qwertyz&units=metric&appid=0000";
            var httpFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.NotFoundResponse);
            var logger = new Mock<ILogger<WeatherDataProviderService>>();
            var deserializeService = new WeatherDataProviderService(httpFactory, logger.Object);

            var result = deserializeService.ParseWeatherData(url, CancellationToken.None);

            Assert.Null(result.Result.Forecasts);
        }

        [Fact]
        public void ServiceReturnsExceptionWhenZipcodeNotValid()
        {
            string url = "https://api.openweathermap.org/data/2.5/forecast?zip=123456,de&units=metric&appid=0000";
            var httpFactory = ClientBuilder.OpenWeatherClientFactory(OpenWeatherResponses.NotFoundResponse);
            var logger = new Mock<ILogger<WeatherDataProviderService>>();
            var deserializeService = new WeatherDataProviderService(httpFactory, logger.Object);

            var result = deserializeService.ParseWeatherData(url, CancellationToken.None);

            Assert.Null(result.Result.Forecasts);
        }
    }
}
