using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using WeatherWatcher.Models;

namespace WeatherWatcher.Tests.Common
{
    public static class OpenWeatherResponses
    {
        public static StringContent OkResponse => BuildOkResponse();
        public static StringContent NotFoundResponse => BuildNotFoundResponse();

        private static StringContent BuildOkResponse()
        {
            var weather = new Weather { WeatherDescription = "couldy" };
            var windData = new WindData { WindSpeed = 2 };
            var mainJsonData = new MainJsonData { Humidity = 67, Temperature = 4 };
            var forecast =
                new Forecast
                {
                    Date = "01/01/2000",
                    MainJsonData = mainJsonData,
                    WindData = windData,
                    Weather = new List<Weather> { weather}
   
                };

            var response = new OpenWeatherResponse
            {
                Forecasts = new List<Forecast> { forecast}
            };


            var json = JsonSerializer.Serialize(response);
            return new StringContent(json);
        }

        private static StringContent BuildNotFoundResponse()
        {
            var json = JsonSerializer.Serialize(new { Cod = 404, Message = "city not found" });
            return new StringContent(json);
        }

    }
}
