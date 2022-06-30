using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientWithUsingSample
{
    internal class Program
    {
        private const string serviceUrl = "https://localhost:5001/";
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                // Using li kullanımda bu satır çok önemli!
                client.DefaultRequestHeaders.ConnectionClose = true;

                #region GetSample
                
                var getResult = await client.GetAsync($"{serviceUrl}api/weatherforecast/GetRandomWeatherForecasts");

                if (getResult.IsSuccessStatusCode)
                {
                    var content = await getResult.Content.ReadAsStringAsync();
                    var weatherForeCasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
                }

                #endregion

                #region PostSample

                var warehouseForecast = new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Jamiryo",
                    TemperatureC = new Random().Next(1, 100)
                };
                string postDataJson = JsonConvert.SerializeObject(warehouseForecast);
                StringContent httpContent = new StringContent(postDataJson, System.Text.Encoding.UTF8, "application/json");

                var postResult = await client.PostAsync($"{serviceUrl}api/weatherforecast/InsertWeatherForecast", httpContent);

                if (postResult.IsSuccessStatusCode)
                {
                    var content = await postResult.Content.ReadAsStringAsync();
                    var weatherForeCasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
                } 

                #endregion
            }
            Console.WriteLine("Hello World!");
        }
    }
}
