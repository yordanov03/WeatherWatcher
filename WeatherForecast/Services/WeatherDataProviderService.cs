using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services
{
    public class WeatherDataProviderService : IWeatherDataProviderService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<WeatherDataProviderService> _logger;

        public WeatherDataProviderService(IHttpClientFactory httpFactory,
            ILogger<WeatherDataProviderService> logger)
        {
            this._httpFactory = httpFactory;
            this._logger = logger;
        }
        public async Task<OpenWeatherResponse> ParseWeatherData(string url, CancellationToken cancellationToken)
        {
            // 1. Make the request
            var client = _httpFactory.CreateClient();
            var response = await client.GetAsync(url, cancellationToken);

            this._logger.LogInformation("Successfully connected to OpenWeatherApi");

            if(response.IsSuccessStatusCode)
            {
                // 2. Deserialize the response.
                var openWeatherResponse = response.Content.ReadFromJsonAsync<OpenWeatherResponse>();
                this._logger.LogInformation("Successfully deserialized API json response");

                return openWeatherResponse.Result;
            }

            else
            {
                response.StatusCode = HttpStatusCode.NoContent;
                this._logger.LogError("Could not fetch data from Api");
                throw new WeatherDataProviderException(response.StatusCode, "Error response from OpenWeatherApi: " + response.ReasonPhrase);
            }
        }

    }
}
