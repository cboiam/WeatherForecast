using AutoMapper;
using System.Collections.Generic;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<(WeatherDto, ThermometricScales), Weather>()
                .ConstructUsing((src, context) => new Weather(src.Item2, 
                    context.Mapper.Map<Location>(src.Item1.Location),
                    context.Mapper.Map<IEnumerable<Forecast>>((src.Item2, src.Item1.Forecasts))));

            CreateMap<(ForecastDto, ThermometricScales), Forecast>()
                .ConstructUsing(src => new Forecast(src.Item2, 
                    src.Item1.ApplicableDate, 
                    src.Item1.MinTemp,
                    src.Item1.MaxTemp));
            
            CreateMap<LocationResumeDto, Location>()
                .ConstructUsing(src => new Location(src.Woeid, src.Title));

            CreateMap<LocationDto, Location>()
                .ConstructUsing(src => new Location(src.Woeid, src.Title, src.LattLong, src.LocationType));
        }
    }
}
