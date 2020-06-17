using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("location/{locationId}/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IGetWeatherByLocationIdUseCase getWeatherByLocationUseCase;

        public WeatherForecastController(IGetWeatherByLocationIdUseCase getWeatherByLocationUseCase)
        {
            this.getWeatherByLocationUseCase = getWeatherByLocationUseCase;
        }

        [HttpGet]
        public IActionResult Get(int locationId, ThermometricScales scale)
        {
            var result = getWeatherByLocationUseCase.Execute(locationId, scale);
            return Ok(result);
        }
    }
}
