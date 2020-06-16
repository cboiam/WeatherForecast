using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Services.Interfaces;
using WeatherForecast.Core.UseCases.Locations.Interfaces;

namespace WeatherForecast.Core.UseCases.Locations
{
    public class SearchLocationUseCase : ISearchLocationUseCase
    {
        private readonly IMetaWeatherService weatherService;
        private readonly IMapper mapper;

        public SearchLocationUseCase(IMetaWeatherService weatherService, IMapper mapper)
        {
            this.weatherService = weatherService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Location>> Execute(string location)
        {
            if(string.IsNullOrWhiteSpace(location))
            {
                throw new BusinessException("Can't search for an empty location!");
            }

            var locations = await weatherService.SearchLocation(location);
            return mapper.Map<IEnumerable<Location>>(locations);
        }
    }
}
