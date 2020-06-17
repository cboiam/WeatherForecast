using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.UseCases.Locations.Interfaces;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("locations")]
    public class LocationController : ControllerBase
    {
        private readonly ISearchLocationUseCase searchLocationUseCase;

        public LocationController(ISearchLocationUseCase searchLocationUseCase)
        {
            this.searchLocationUseCase = searchLocationUseCase;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string term)
        {
            var result = await searchLocationUseCase.Execute(term);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
