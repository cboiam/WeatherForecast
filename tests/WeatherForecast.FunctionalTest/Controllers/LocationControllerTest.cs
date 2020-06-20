using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Text.Json;
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

            var result = await client.GetAsync("/locations/search?term=location");
            
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            var content = await result.Content.ReadAsStringAsync();
            var expectedContent = JsonSerializer.Serialize(new List<Location>
            {
                new Location(1, "location")
            }, jsonSerializerOptions);

            content.Should().Be(expectedContent);
        }
    }
}
