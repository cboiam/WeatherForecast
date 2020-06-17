using AutoMapper;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Services.Interfaces;
using WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces;

namespace WeatherForecast.Core.UseCases.WeatherForecasts
{
    public class GetWeatherByLocationIdUseCase : IGetWeatherByLocationIdUseCase
    {
        private readonly IMetaWeatherService weatherService;
        private readonly IMapper mapper;

        public GetWeatherByLocationIdUseCase(IMetaWeatherService weatherService, IMapper mapper)
        {
            this.weatherService = weatherService;
            this.mapper = mapper;
        }

        public async Task<Weather> Execute(int locationId, ThermometricScales scale)
        {
            var weatherForecast = await weatherService.GetWeatherForecast(locationId);
            return mapper.Map<Weather>((weatherForecast, scale));
        }
    }
}
