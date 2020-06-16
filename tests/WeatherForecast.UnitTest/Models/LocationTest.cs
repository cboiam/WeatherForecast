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
            var location = new Location(1, "Location", "51.506321,-0.12714");

            location.Latitude.Should().Be(51.506321f);
            location.Longitude.Should().Be(-0.12714f);
        }

        [Fact]
        public void ConstructLocation_ShouldThrowFormatException_WhenPoorlyFormattedGeolocation()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ConstructLocation_ShouldThrowException_WhenLiteralsInGeolocation()
        {
            new Location(1, "Location", "51.506321,-0.12ab714");
        }
    }
}
