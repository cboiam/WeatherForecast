using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<(LocationWeatherDto, ThermometricScales), Weather>()
                .ConstructUsing((src, ctx) => new Weather(src.Item2, 
                    ctx.Mapper.Map<Location>(src.Item1),
                    ctx.Mapper.Map<IEnumerable<Forecast>>((src.Item1.ConsolidatedWeather, src.Item2))));

            CreateMap<(IEnumerable<ForecastDto>, ThermometricScales), IEnumerable<Forecast>>()
                .ConvertUsing((src, dest, ctx) => src.Item1.Select(f => 
                    ctx.Mapper.Map<Forecast>((f, src.Item2))));

            CreateMap<(ForecastDto, ThermometricScales), Forecast>()
                .ConstructUsing(src => new Forecast(src.Item2, 
                    src.Item1.ApplicableDate, 
                    src.Item1.MinTemp,
                    src.Item1.MaxTemp));
            
            CreateMap<LocationResumeDto, Location>()
                .ConstructUsing(src => new Location(src.Woeid, src.Title));
        }
    }
}
