using System;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Entities
{
    public class Temperature
    {
        /// <param name="temperature">Temperature in Celcius</param>
        public Temperature(double temperature)
        {
            TemperatureC = temperature;
        }

        private double TemperatureC { get; }

        private double TemperatureF => 32 + (TemperatureC / 0.5556);

        private double TemperatureK => 273 + TemperatureC;

        public double Evaluate(ThermometricScales scale) => scale switch
        {
            ThermometricScales.Celsius => TemperatureC,
            ThermometricScales.Fahrenheit => TemperatureF,
            ThermometricScales.Kelvin => TemperatureK,
            _ => throw new NotImplementedException("This scale doesn't exist!")
        };
    }
}