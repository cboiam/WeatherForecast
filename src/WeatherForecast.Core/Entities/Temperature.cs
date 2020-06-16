using System;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Entities
{
    public class Temperature
    {
        public Temperature(double temperature)
        {
            TemperatureC = temperature;
        }

        public double TemperatureC { get; private set; }

        public double TemperatureF => 32 + (TemperatureC / 0.56);

        public double TemperatureK => 237 + TemperatureC;

        public double Evaluate(ThermometricScales scale) => scale switch
        {
            ThermometricScales.Fahrenheit => TemperatureF,
            ThermometricScales.Kelvin => TemperatureK,
            _ => throw new NotImplementedException("This scale doesn't exist!")
        };
    }
}