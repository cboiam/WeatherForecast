using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using WeatherForecast.Api;
using WireMock.Server;
using Xunit;

namespace WeatherForecast.FunctionalTest.Controllers
{
    public class ControllerBaseTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient client;
        protected readonly JsonSerializerSettings jsonSerializerSettings;
        protected static FluentMockServer server;

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

            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            if (server == null)
            {
                server = FluentMockServer.Start("http://localhost:7080");
            }
        }
    }
}
