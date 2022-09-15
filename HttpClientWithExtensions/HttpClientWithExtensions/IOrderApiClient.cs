using HttpClientWithExtensions.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientWithExtensions
{
    public interface IOrderApiClient
    {
        Task<List<WeatherForecast>> GetRandomWeatherForecasts();
        Task<List<WeatherForecast>> InsertWeatherForecast(WeatherForecast weatherForecast);
        Task<List<WeatherForecast>> GenerateVeryLargeWeatherForecasts(GenerateVeryLargeWeatherForecastRequest request);
    }
}
