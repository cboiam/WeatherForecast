using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherForecast.Api;
using WireMock.Server;
using Xunit;

namespace WeatherForecast.FunctionalTest.Controllers
{
    public class ControllerBaseTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient client;
        protected readonly JsonSerializerOptions jsonSerializerOptions;
        protected static FluentMockServer server;

        static ControllerBaseTest()
        {
            server = FluentMockServer.Start("http://localhost:7080");
        }

        public ControllerBaseTest(WebApplicationFactory<Startup> factory)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            client = factory.WithWebHostBuilder(x =>
            {
                x.UseEnvironment("Test");
                x.UseConfiguration(configuration);
            }).CreateClient();

            jsonSerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
