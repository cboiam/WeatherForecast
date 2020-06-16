using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace WeatherForecast.Api.Configurations
{
    public static class JsonConfiguration
    {
        public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder)
        {
            return builder.AddJsonOptions(opt =>
             {
                 opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 opt.JsonSerializerOptions.IgnoreNullValues = true;
             });
        }
    }
}
