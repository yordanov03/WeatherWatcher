using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services.Contracts
{
    public interface IWeatherDataProviderService
    {
        Task<OpenWeatherResponse> ParseWeatherData(string url, CancellationToken cancellationToken);
    }
}
