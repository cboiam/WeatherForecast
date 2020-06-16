using FluentAssertions;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Entities;
using Xunit;

namespace WeatherForecast.UnitTest.Models
{
    public class TemperatureTest
    {
        [Fact]
        public void Temperature0_ShouldBe32InFahrenheit() =>
            new Temperature(0).TemperatureF.Should().Be(32);

        [Fact]
        public void Temperature100_ShouldBe212InFahrenheit() =>
            new Temperature(100).TemperatureF.Should().Be(212);

        [Fact]
        public void Evaluate_ShouldReturnCelciusTemperature() =>
            new Temperature(0).Evaluate(ThermometricScales.Celsius).Should().Be(0);

        [Fact]
        public void Evaluate_ShouldReturnFahrenheitTemperature() =>
            new Temperature(0).Evaluate(ThermometricScales.Fahrenheit).Should().Be(32);

        [Fact]
        public void Evaluate_ShouldReturnKelvinTemperature() =>
            new Temperature(0).Evaluate(ThermometricScales.Kelvin).Should().Be(273);
    }
}
