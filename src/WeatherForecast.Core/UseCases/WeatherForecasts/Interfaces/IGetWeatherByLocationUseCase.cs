using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Entities;
using System.Threading.Tasks;

namespace WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces
{
    public interface IGetWeatherByLocationIdUseCase
    {
        Task<Weather> Execute(int locationId, ThermometricScales scale);
    }
}
