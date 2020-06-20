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
            var weatherService = new Mock<IMetaWeatherService>();
            weatherService.Setup(x => x.SearchLocation("location"))
                .ReturnsAsync(new List<LocationResumeDto>
                {
                    new LocationResumeDto { Woeid = 1, Title = "loc" },
                    new LocationResumeDto { Woeid = 2, Title = "ation" }
                });

            var useCase = new SearchLocationUseCase(weatherService.Object, Mapper.Instance);
            var result = await useCase.Execute("location");

            result.Should().BeEquivalentTo(new List<Location>
            {
                new Location(1, "loc"),
                new Location(2, "ation")
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Execute_ShouldThrowBusinessException_WhenEmptyLocation(string location)
        {
            var weatherService = new Mock<IMetaWeatherService>();

            var useCase = new SearchLocationUseCase(weatherService.Object, Mapper.Instance);
            Action action = () => useCase.Execute(location).Wait();

            action.Should().Throw<BusinessException>()
                .WithMessage("Can't search for an empty location!");
        }
    }
}
