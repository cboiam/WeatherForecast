using AutoMapper;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.Core.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<LocationResumeDto, Location>()
                .ConstructUsing(src => new Location(src.Woeid, src.Title));
        }
    }
}
