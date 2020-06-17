using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Api;
using WeatherForecast.Core.Entities;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace WeatherForecast.FunctionalTest.Controllers
{
    public class LocationControllerTest : ControllerBaseTest
    {
        public LocationControllerTest(WebApplicationFactory<Startup> factory) 
            : base(factory) { }

        [Fact]
        public async Task SearchLocation_ShouldReturn200WithLocations_WhenLocationsFound()
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
            var result = await client.GetAsync("/locations/search?term=location");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            var content = await result.Content.ReadAsStringAsync();
            var expectedContent = JsonConvert.SerializeObject(new List<Location>
            {
                new Location(1, "location")
            }, jsonSerializerSettings);

            content.Should().Be(expectedContent);
        }
    }
}
