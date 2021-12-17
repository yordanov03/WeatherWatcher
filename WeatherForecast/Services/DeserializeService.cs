using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services
{
    public class DeserializeService : IDeserializeService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<DeserializeService> _logger;

        public DeserializeService(IHttpClientFactory httpFactory,
            ILogger<DeserializeService> logger)
        {
            this._httpFactory = httpFactory;
            this._logger = logger;
        }
        public async Task<OpenWeatherResponse> DeserializeJson(string url, CancellationToken cancellationToken)
        {
            // 1. Make the request
            var client = _httpFactory.CreateClient();
            var response = await client.GetAsync(url, cancellationToken);

            this._logger.LogInformation("Successfully connected to OenWeatherApi");

            if(response.IsSuccessStatusCode)
            {
                // 2. Deserialize the response.
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var jsonOptions = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
                var openWeatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(json, jsonOptions);
                this._logger.LogInformation("Successfully deserialized API json response");

                return openWeatherResponse;
            }

            else
            {
                this._logger.LogError("Could not map response to a model");
                throw new DeserializeException(response.StatusCode, "Error response from OpenWeatherApi: " + response.ReasonPhrase);
            }
        }

    }
}
