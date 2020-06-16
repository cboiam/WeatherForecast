using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core.Dtos;

namespace WeatherForecast.Core.Services.Interfaces
{
    public interface IMetaWeatherService
    {
        public Task<IEnumerable<LocationResumeDto>> SearchLocation(string location);
    }
}
