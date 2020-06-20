using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> GetWeatherForecast(int locationId, [FromQuery] ThermometricScales scale)
        {
            var result = await getWeatherByLocationUseCase.Execute(locationId, scale);

            if (result == null)
            {
                return NotFound();
            }

            if (result.Forecasts == null || !result.Forecasts.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
