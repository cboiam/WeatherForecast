using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecast.Api;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Enums;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace WeatherForecast.FunctionalTest.Controllers
{
    public class WeatherForecastControllerTest : ControllerBaseTest
    {
        public WeatherForecastControllerTest(WebApplicationFactory<Startup> factory) 
            : base(factory) { }

        [Fact]
        public async Task GetWeatherForecast_ShouldReturnWeather_WhenExecutedSuccessfully()
        {
            server.Given(Request.Create()
                    .UsingGet()
                    .WithPath("/api/location/1"))
                .RespondWith(Response.Create()
                    .WithBody(@"{
                                    ""woeid"": 1,
                                    ""title"": ""Location"",
                                    ""latt_long"": ""1,2"",
                                    ""location_type"": ""City"",
                                    ""consolidated_weather"": [
                                        {
                                            ""min_temp"": 1,
                                            ""max_temp"": 2,
                                            ""applicable_date"": ""2020-06-20""
                                        },
                                        {
                                            ""min_temp"": 3,
                                            ""max_temp"": 4,
                                            ""applicable_date"": ""2020-06-21""
                                        }
                                    ]
                                }")
                    .WithSuccess());

            var response = await client.GetAsync("locations/1/weather");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var expectedContent = JsonSerializer.Serialize(new Weather(ThermometricScales.Celsius,
                    new Location(1, "Location", "1,2", "City"),
                    new List<Forecast>
                    { 
                        new Forecast(ThermometricScales.Celsius, new DateTime(2020, 06, 20), 1, 2),
                        new Forecast(ThermometricScales.Celsius, new DateTime(2020, 06, 21), 3, 4)
                    }
                ), jsonSerializerOptions);

            content.Should().Be(expectedContent);
        }
    }
}
