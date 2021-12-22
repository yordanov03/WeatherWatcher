using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WeatherWatcher.Api.ExceptionFilters;
using WeatherWatcher.Api.Models;
using WeatherWatcher.Services.Contracts;

namespace WeatherWatcher.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(ForecastServiceExceptionFilter))]
    public class WeatherController : ControllerBase
    {
        private readonly IForecastService _weatherService;

        public WeatherController(IForecastService weatherService)
        {
            this._weatherService = weatherService;
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] SearchParams searchKey, CancellationToken cancellationToken)
        {

            var forecast = !string.IsNullOrEmpty(searchKey.City) ?
                await this._weatherService.GetForecastByCityName(searchKey.City, cancellationToken) :
                await this._weatherService.GetForecastByZipCode(searchKey.ZipCode, cancellationToken);

            return Ok(forecast);
        }
    }
}
