using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.UseCases.Locations.Interfaces;
using Xunit;

namespace WeatherForecast.UnitTest.Controllers
{
    public class LocationControllerTest
    {
        [Fact]
        public async Task Search_ShouldReturnLocations_WhenLocationsFound()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location(1, "loc"),
                new Location(2, "ation")
            };

            var useCase = new Mock<ISearchLocationUseCase>();
            useCase.Setup(x => x.Execute("location"))
                .ReturnsAsync(locations);

            // Act
            var controller = new LocationController(useCase.Object);
            var result = await controller.Search("location");

            // Assert
            var objectResultAssertion = result.Should().BeOfType<OkObjectResult>();

            objectResultAssertion.Which.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResultAssertion.Which.Value.Should().BeEquivalentTo(locations);
        }

        [Fact]
        public async Task Search_ShouldReturnNoContent_WhenNoLocationsResultIsNull()
        {
            var controller = new LocationController(new Mock<ISearchLocationUseCase>().Object);
            var result = await controller.Search("location");

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Search_ShouldReturnNoContent_WhenNoLocationsWereFound()
        {
            var useCase = new Mock<ISearchLocationUseCase>();
            useCase.Setup(x => x.Execute("location"))
                .ReturnsAsync(new List<Location>());

            var controller = new LocationController(useCase.Object);
            var result = await controller.Search("location");

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
