using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Services.Interfaces;
using WeatherForecast.Core.UseCases.WeatherForecasts;
using Xunit;

namespace WeatherForecast.UnitTest.UseCases.WeatherForecasts
{
    public class GetWeatherByLocationIdUseCaseTest
    {
        [Fact]
        public async Task Execute_ShouldReturnWeather_WhenSuccess()
        {
            var service = new Mock<IMetaWeatherService>();
            service.Setup(x => x.GetWeatherForecast(1))
                .ReturnsAsync(new LocationWeatherDto
                {
                    Woeid = 1,
                    LattLong = "1,2",
                    LocationType = "City",
                    Title = "Location",
                    ConsolidatedWeather = new List<ForecastDto>
                    {
                        new ForecastDto
                        {
                            MinTemp = 1,
                            MaxTemp = 2,
                            ApplicableDate = DateTime.Today
                        }
                    }
                });

            var useCase = new GetWeatherByLocationIdUseCase(service.Object, Mapper.Instance);
            var result = await useCase.Execute(1, ThermometricScales.Celsius);

            result.Should().BeEquivalentTo(
                new Weather(ThermometricScales.Celsius,
                            new Location(1, "Location", "1,2", "City"),
                            new List<Forecast>
                            {
                                new Forecast(ThermometricScales.Celsius, DateTime.Today, 1, 2)
                            }));
        }
    }
}
