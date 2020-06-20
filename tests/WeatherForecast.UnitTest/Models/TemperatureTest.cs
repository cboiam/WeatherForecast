using FluentAssertions;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Entities;
using Xunit;
using System;

namespace WeatherForecast.UnitTest.Models
{
    public class TemperatureTest
    {
        [Fact]
        public void Temperature0_ShouldBe32InFahrenheit() =>
            new Temperature(0).Evaluate(ThermometricScales.Fahrenheit)
                .Should().Be(32);

        [Fact]
        public void Temperature100_ShouldBe212InFahrenheit() =>
            new Temperature(100).Evaluate(ThermometricScales.Fahrenheit)
                .Should().BeApproximately(212, 2);

        [Fact]
        public void Temperature0_ShouldBe273InKelvinTemperature() =>
            new Temperature(0).Evaluate(ThermometricScales.Kelvin)
                .Should().Be(273);

        [Fact]
        public void Temperature100_ShouldBe373InKelvinTemperature() =>
            new Temperature(100).Evaluate(ThermometricScales.Kelvin)
                .Should().Be(373);

        [Fact]
        public void Evaluate_ShouldReturnCelciusTemperature() =>
            new Temperature(0).Evaluate(ThermometricScales.Celsius)
                .Should().Be(0);

        [Fact]
        public void Evaluate_ShouldThrowNotImplementedException_WhenNotKnownThermometricScale()
        {
            Action action = () => new Temperature(0).Evaluate((ThermometricScales)99);

            action.Should().Throw<NotImplementedException>()
                .WithMessage("This scale doesn't exist!");
        }
            
    }
}
