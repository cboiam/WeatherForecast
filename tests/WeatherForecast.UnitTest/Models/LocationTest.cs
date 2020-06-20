using FluentAssertions;
using System;
using WeatherForecast.Core.Entities;
using Xunit;

namespace WeatherForecast.UnitTest.Models
{
    public class LocationTest
    {
        [Fact]
        public void ConstructLocation_ShouldSucceed_WhenWellFormattedGeolocalization()
        {
            var location = new Location(1, "Location", "51.506321,-0.12714", "city");

            location.Id.Should().Be(1);
            location.Name.Should().Be("Location");
            location.Latitude.Should().Be(51.506321f);
            location.Longitude.Should().Be(-0.12714f);
            location.Type.Should().Be("city");
        }

        [Theory]
        [InlineData("")]
        [InlineData("123123")]
        [InlineData("123,123,123")]
        public void ConstructLocation_ShouldThrowFormatException_WhenPoorlyFormattedGeolocation(string geolocation)
        {
            Action action = () => new Location(1, "Location", geolocation, "city");

            action.Should().Throw<FormatException>()
                .WithMessage("Geolocation should have the format: '[latitude],[longitude]'");
        }

        [Fact]
        public void ConstructLocation_ShouldThrowException_WhenLiteralsInGeolocation()
        {
            Action action = () => new Location(1, "Location", "51.506321,-0.12ab714", "city");

            action.Should().Throw<FormatException>();
        }
    }
}
