using System.Collections.Generic;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Entities
{
    public class Weather
    {
        public Weather(ThermometricScales scale, Location location, IEnumerable<Forecast> forecasts)
        {
            Scale = scale;
            Location = location;
            Forecasts = forecasts;
        }

        public ThermometricScales Scale { get; }
        public Location Location { get; private set; }
        public IEnumerable<Forecast> Forecasts { get; set; }
    }
}
