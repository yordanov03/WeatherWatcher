using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Dtos;

namespace WeatherWatcher.Services.Contracts
{
    public interface IForecastService
    {
        Task<List<WeatherForecastDto>> GetForecastByCityName(string location, CancellationToken cancelationtoken);
        Task<List<WeatherForecastDto>> GetForecastByZipCode(string zipCode, CancellationToken cancelationtoken);
    }
}
