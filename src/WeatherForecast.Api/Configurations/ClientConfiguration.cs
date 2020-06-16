using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WeatherForecast.Core.Services;
using WeatherForecast.Core.Services.Interfaces;

namespace WeatherForecast.Api.Configurations
{
    public static class ClientConfiguration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IMetaWeatherService, MetaWeatherService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("MetaWeatherApiUrl"));
            });

            return services;
        }
    }
}
