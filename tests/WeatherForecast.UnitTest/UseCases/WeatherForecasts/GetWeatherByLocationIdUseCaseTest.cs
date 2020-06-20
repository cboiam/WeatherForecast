using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Dtos;
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
                    ConsolidatedWeather = new List<ForecastDto>
                    {
                        new ForecastDto {
                            MinTemp = 1,
                            MaxTemp = 2, 
                            ApplicableDate = DateTime.Today 
                        }
                    },
                    LattLong = "1,2",
                    LocationType = "City",
                    Title = "Location"
                });

            var useCase = new GetWeatherByLocationIdUseCase(service.Object, Mapper.Instance);
            var result = await useCase.Execute(1, ThermometricScales.Celsius);

            result.Location.Id.Should().Be(1);
            result.Location.Name.Should().Be("Location");
            result.Location.Latitude.Should().Be(1);
            result.Location.Longitude.Should().Be(2);

            var forecast = result.Forecasts.First();
                
            forecast.MinTemperature.Should().Be(1);
            forecast.MaxTemperature.Should().Be(2);
            forecast.Date.Should().Be(DateTime.Today);
        }
    }
}
