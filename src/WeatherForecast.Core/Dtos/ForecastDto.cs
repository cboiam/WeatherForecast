using System;

namespace WeatherForecast.Core.Dtos
{
    public class ForecastDto
    {
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
        public DateTime ApplicableDate { get; set; }
    }
}
