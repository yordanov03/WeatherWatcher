using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services.Contracts
{
    public interface IOpenWeatherService
    {
        Task<List<WeatherForecast>> GetWeatherForecast(string url, CancellationToken cancellationToken);
    }
}
