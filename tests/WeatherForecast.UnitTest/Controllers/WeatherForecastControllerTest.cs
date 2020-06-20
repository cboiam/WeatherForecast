using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.UseCases.WeatherForecasts.Interfaces;
using Xunit;

namespace WeatherForecast.UnitTest.Controllers
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public async Task GetWeatherForecast_ReturnsWeatherWithStatusOk_WhenFound()
        {
            var expectedResult = new Weather(ThermometricScales.Celsius, 
                                             new Location(1, "location", "1,2", "City"),
                                             new List<Forecast>
                                             {
                                                 new Forecast(ThermometricScales.Celsius, DateTime.Today, 1, 2)
                                             });

            var useCase = new Mock<IGetWeatherByLocationIdUseCase>();
            useCase.Setup(x => x.Execute(1, ThermometricScales.Celsius))
                .ReturnsAsync(expectedResult);

            var controller = new WeatherForecastController(useCase.Object);
            var result = await controller.GetWeatherForecast(1, ThermometricScales.Celsius);

            var objectResultAssertion = result.Should().BeOfType<OkObjectResult>();

            objectResultAssertion.Which.StatusCode.Should()
                .Be(StatusCodes.Status200OK);

            objectResultAssertion.Which.Value.Should()
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsNotFount_WhenNoWeatherFound()
        {
            var controller = new WeatherForecastController(new Mock<IGetWeatherByLocationIdUseCase>().Object);
            var result = await controller.GetWeatherForecast(1, ThermometricScales.Celsius);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsNoContent_WhenForecastsAreNull()
        {
            var useCase = new Mock<IGetWeatherByLocationIdUseCase>();
            useCase.Setup(x => x.Execute(1, ThermometricScales.Celsius))
                .ReturnsAsync(new Weather(ThermometricScales.Celsius, 
                                          new Location(1, "location", "1,2", "City"),
                                          null));

            var controller = new WeatherForecastController(useCase.Object);
            var result = await controller.GetWeatherForecast(1, ThermometricScales.Celsius);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsNoContent_WhenForecastsIsEmpty()
        {
            var useCase = new Mock<IGetWeatherByLocationIdUseCase>();
            useCase.Setup(x => x.Execute(1, ThermometricScales.Celsius))
                .ReturnsAsync(new Weather(ThermometricScales.Celsius,
                                          new Location(1, "location", "1,2", "City"),
                                          new List<Forecast>()));

            var controller = new WeatherForecastController(useCase.Object);
            var result = await controller.GetWeatherForecast(1, ThermometricScales.Celsius);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
