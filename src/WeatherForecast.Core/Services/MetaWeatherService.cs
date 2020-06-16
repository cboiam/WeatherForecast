using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Core.Dtos;
using WeatherForecast.Core.Services.Interfaces;

namespace WeatherForecast.Core.Services
{
    public class MetaWeatherService : IMetaWeatherService
    {
        private readonly HttpClient client;

        public MetaWeatherService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<LocationResumeDto>> SearchLocation(string location)
        {
            var response = await client.GetAsync($"api/location/search/?query={location}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<LocationResumeDto>>(content, JsonSerializerSettings);
            }

            throw new ApplicationException("Third party service not available or running with errors, contact administrators for more feedback!");
        }

        private JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}
