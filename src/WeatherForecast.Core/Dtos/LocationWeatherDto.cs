using System.Collections.Generic;

namespace WeatherForecast.Core.Dtos
{
    public class LocationWeatherDto : LocationResumeDto
    {
        public string LattLong { get; set; }
        public string LocationType { get; set; }
        public IEnumerable<ForecastDto> ConsolidatedWeather { get; set; }
    }
}
