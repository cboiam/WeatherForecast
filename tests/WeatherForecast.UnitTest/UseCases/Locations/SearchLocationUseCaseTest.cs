using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Services.Interfaces;
using WeatherForecast.Core.UseCases.Locations;
using Xunit;

namespace WeatherForecast.UnitTest.UseCases.Locations
{
    public class SearchLocationUseCaseTest
    {
        [Fact]
        public async Task Execute_ShouldReturnLocations()
        {
            //Arrange
            var weatherService = new Mock<IMetaWeatherService>();
            weatherService.Setup(x => x.SearchLocation("location"))
                .ReturnsAsync(new List<LocationResumeDto>
                {
                    new LocationResumeDto { Woeid = 1, Title = "loc" },
                    new LocationResumeDto { Woeid = 2, Title = "ation" }
                });

            // Act
            var useCase = new SearchLocationUseCase(weatherService.Object, Mapper.Instance);
            var result = await useCase.Execute("location");

            // Assert
            result.Should().BeEquivalentTo(new List<Location>
            {
                new Location(1, "loc"),
                new Location(2, "ation")
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Execute_ShouldThrowBusinessException_WhenEmptyLocation(string location)
        {
            //Arrange
            var weatherService = new Mock<IMetaWeatherService>();

            // Act
            var useCase = new SearchLocationUseCase(weatherService.Object, Mapper.Instance);
            Action action = async () => await useCase.Execute(location);

            // Assert
            action.Should().Throw<BusinessException>()
                .WithMessage("Can't search for an empty location!");
        }
    }
}
