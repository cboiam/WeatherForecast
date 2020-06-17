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
            // Arrange
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

            // Act
            var locations = await service.SearchLocation("location");
            
            // Assert
            locations.Should().BeEquivalentTo(new List<LocationResumeDto>
            {
                new LocationResumeDto { Woeid = 1, Title = "location" }
            });
        }

        [Fact]
        public void SearchLocation_ShouldThrowApplicationException_WhenErrorStatusCode()
        {
            // Arrange
            server.Given(Request.Create()
                            .UsingGet()
                            .WithPath("/api/location/search")
                            .WithParam("query", new ExactMatcher("error")))
                   .RespondWith(Response.Create()
                            .WithStatusCode(StatusCodes.Status500InternalServerError));

            // Act
            Action action = () => service.SearchLocation("error").Wait();
            
            // Assert
            action.Should().Throw<ApplicationException>()
                .WithMessage("Third party service not available or running with errors, contact administrators for more feedback!");
        }
    }
}