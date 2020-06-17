using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core.Dtos;

namespace WeatherForecast.Core.Services.Interfaces
{
    public interface IMetaWeatherService
    {
        Task<IEnumerable<LocationResumeDto>> SearchLocation(string location);

        Task<LocationWeatherDto> GetWeatherForecast(int locationId);
    }
}
