using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.Core.UseCases.Locations.Interfaces
{
    public interface ISearchLocationUseCase
    {
        public Task<IEnumerable<Location>> Execute(string location);
    }
}
