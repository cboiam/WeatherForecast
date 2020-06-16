using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Core.Mappings;

namespace WeatherForecast.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(DtoToEntityProfile));
        }
    }
}
