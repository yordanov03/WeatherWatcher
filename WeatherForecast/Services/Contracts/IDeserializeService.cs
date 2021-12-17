using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Services.Contracts
{
    public interface IDeserializeService
    {
        Task<OpenWeatherResponse> DeserializeJson(string url, CancellationToken cancellationToken);
    }
}
