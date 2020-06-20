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
        private readonly ApplicationException serviceNotAvailableException = new ApplicationException("Third party service not available or running with errors, contact administrators for more feedback!");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public MetaWeatherService(IHttpClientFactory client)
        {
            this.client = client.CreateClient("MetaWeatherApiUrl");
        }

        public async Task<IEnumerable<LocationResumeDto>> SearchLocation(string location)
        {
            var response = await client.GetAsync($"api/location/search?query={location}");

            if (!response.IsSuccessStatusCode)
            {
                throw serviceNotAvailableException;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<LocationResumeDto>>(content, jsonSerializerSettings);
        }

        public async Task<LocationWeatherDto> GetWeatherForecast(int locationId)
        {
            var response = await client.GetAsync($"api/location/{locationId}");

            if (!response.IsSuccessStatusCode)
            {
                throw serviceNotAvailableException;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LocationWeatherDto>(content, jsonSerializerSettings);
        }
    }
}
