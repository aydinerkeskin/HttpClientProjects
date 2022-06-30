using HttpClientWithExtensions.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientWithExtensions
{
    public class OrderApiClient : IOrderApiClient
    {
        public static readonly JsonSerializerOptions OptionFilters = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private readonly HttpClient _httpClient;

        public OrderApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherForecast>> GetRandomWeatherForecasts()
        {
            var path = $"api/weatherforecast/GetRandomWeatherForecasts";

            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(path);
        }

        public async Task<List<WeatherForecast>> InsertWeatherForecast(WeatherForecast weatherForecast)
        {
            var path = $"api/weatherforecast/InsertWeatherForecast";

            var httpResponseMessage = await _httpClient.PostAsJsonAsync(path, weatherForecast);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await httpResponseMessage.Content.ReadFromJsonAsync<List<WeatherForecast>>();
            }

            return null;
        }
    }
}
