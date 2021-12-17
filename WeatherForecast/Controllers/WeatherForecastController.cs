using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.Common;
using WeatherWatcher.Api.Models;
using WeatherWatcher.Services.Contracts;

namespace WeatherWatcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IForecastService _weatherService;

        public WeatherForecastController(IForecastService weatherService)
        {
            this._weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] SearchKey searchKey, CancellationToken cancellationToken)
        {
            var forecast = !string.IsNullOrEmpty(searchKey.City) ?
                await this._weatherService.GetForecastByCityName(searchKey.City, cancellationToken) :
                await this._weatherService.GetForecastByZipCode(searchKey.ZipCode, cancellationToken);

            return Ok(forecast);
        }
    }
}
