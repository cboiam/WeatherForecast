using System.Collections.Generic;

namespace WeatherForecast.Core.Dtos
{
    public class WeatherDto
    {
        public LocationDto Location { get; set; }
        public IEnumerable<ForecastDto> Forecasts { get; set; }
    }
}
