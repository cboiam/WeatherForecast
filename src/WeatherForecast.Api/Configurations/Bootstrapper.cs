using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Core.Services;
using WeatherForecast.Core.Services.Interfaces;
using WeatherForecast.Core.UseCases.Locations;
using WeatherForecast.Core.UseCases.Locations.Interfaces;

namespace WeatherForecast.Api.Configurations
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            return services.RegisterUseCases()
                .RegisterServices();
        }

        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            return services.AddScoped<ISearchLocationUseCase, SearchLocationUseCase>();
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.AddScoped<IMetaWeatherService, MetaWeatherService>();
        }
    }
}
