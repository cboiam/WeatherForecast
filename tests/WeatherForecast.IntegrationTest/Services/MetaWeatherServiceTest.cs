using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Services;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace WeatherForecast.IntegrationTest.Services
{
    public class MetaWeatherServiceTest
    {
        private readonly MetaWeatherService service;
        private static FluentMockServer server;

        public MetaWeatherServiceTest()
        {
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient("MetaWeatherApiUrl"))
                .Returns(new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:7070")
                });

            service = new MetaWeatherService(clientFactory.Object);

            if(server == null)
            {
                server = FluentMockServer.Start("http://localhost:7070");
            }
        }

        [Fact]
        public async Task SearchLocation_ShouldReturnLocations_WhenSuccessStatusCode()
        {
            server.Given(Request.Create()
                            .UsingGet()
                            .WithPath("/api/location/search")
                            .WithParam("query", new ExactMatcher("location")))
                   .RespondWith(Response.Create()
                            .WithBody(@"[
                                            {
                                                ""woeid"": 1,
                                                ""title"": ""location"",
                                                ""latt_long"": ""123,456"",
                                                ""location_type"": ""City""
                                            }
                                        ]")
                            .WithSuccess());

            var locations = await service.SearchLocation("location");
            
            locations.Should().BeEquivalentTo(new List<LocationResumeDto>
            {
                new LocationResumeDto { Woeid = 1, Title = "location" }
            });
        }

        [Fact]
        public void SearchLocation_ShouldThrowApplicationException_WhenErrorStatusCode()
        {
            server.Given(Request.Create()
                    .UsingGet()
                    .WithPath("/api/location/search")
                    .WithParam("query", new ExactMatcher("error")))
                .RespondWith(Response.Create()
                    .WithStatusCode(StatusCodes.Status500InternalServerError));

            Action action = () => service.SearchLocation("error").Wait();
            
            action.Should().Throw<ApplicationException>()
                .WithMessage("Third party service not available or running with errors, contact administrators for more feedback!");
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsLocationWeather_WhenSuccess()
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

            var result = await service.GetWeatherForecast(1);

            result.Should().BeEquivalentTo(new LocationWeatherDto
            {
                Woeid = 1,
                Title = "Location",
                LattLong = "1,2",
                LocationType = "City",
                ConsolidatedWeather = new List<ForecastDto>
                { 
                    new ForecastDto { MinTemp = 1, MaxTemp = 2, ApplicableDate = new DateTime(2020, 06, 20) },
                    new ForecastDto { MinTemp = 3, MaxTemp = 4, ApplicableDate = new DateTime(2020, 06, 21) }
                }
            });
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsNull_WhenNoLocationDoesntExist()
        {
            server.Given(Request.Create()
                    .UsingGet()
                    .WithPath("/api/location/2"))
                .RespondWith(Response.Create()
                    .WithStatusCode(StatusCodes.Status404NotFound));

            var result = await service.GetWeatherForecast(2);

            result.Should().BeNull();
        }

        [Fact]
        public void GetWeatherForecast_ThrowsApplicationException_WhenServiceRespondWithError()
        {
            server.Given(Request.Create()
                    .UsingGet()
                    .WithPath("/api/location/3"))
                .RespondWith(Response.Create()
                    .WithStatusCode(StatusCodes.Status500InternalServerError));

            Action action = () => service.GetWeatherForecast(3).Wait();

            action.Should().Throw<ApplicationException>()
                .WithMessage("Third party service not available or running with errors, contact administrators for more feedback!");
        }
    }
}