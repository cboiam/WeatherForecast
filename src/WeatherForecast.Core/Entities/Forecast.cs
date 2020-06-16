using System;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Entities
{
    public class Forecast
    {
        public Forecast(ThermometricScales scale, DateTime date, int minTemperature, int maxTemperature)
        {
            Scale = scale;
            Date = date;
            this.minTemperature = new Temperature(minTemperature);
            this.maxTemperature = new Temperature(maxTemperature);
        }

        private ThermometricScales Scale { get; }

        public DateTime Date { get; private set; }

        private Temperature minTemperature;
        public double MinTemperature => minTemperature.Evaluate(Scale);

        private Temperature maxTemperature;
        public double MaxTemperature => maxTemperature.Evaluate(Scale);
    }
}
