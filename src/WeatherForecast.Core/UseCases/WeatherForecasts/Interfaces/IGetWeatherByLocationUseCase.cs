using System.Collections.Generic;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces
{
    public interface IGetWeatherByLocationIdUseCase
    {
        IEnumerable<Weather> Execute(int locationId, ThermometricScales scale);
    }
}
