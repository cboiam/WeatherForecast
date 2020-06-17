using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("locations/{locationId}/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IGetWeatherByLocationIdUseCase getWeatherByLocationUseCase;

        public WeatherForecastController(IGetWeatherByLocationIdUseCase getWeatherByLocationUseCase)
        {
            this.getWeatherByLocationUseCase = getWeatherByLocationUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int locationId, [FromQuery]ThermometricScales scale)
        {
            var result = await getWeatherByLocationUseCase.Execute(locationId, scale);
            return Ok(result);
        }
    }
}
