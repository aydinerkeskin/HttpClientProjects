using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientWithExtensions.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOrderApiClient _orderApiClient;

        public WeatherForecastController(IOrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }

        [HttpGet("GetRandomWeatherForecasts")]
        public async Task<List<WeatherForecast>> GetRandomWeatherForecasts()
        {
            var serviceResult = await _orderApiClient.GetRandomWeatherForecasts();

            return serviceResult.Select(s => new WeatherForecast { 
                Date = s.Date,
                Summary = s.Summary,
                TemperatureC = s.TemperatureC
            }).ToList();
        }

        [HttpGet("InsertWeatherForecast")]
        public async Task<List<WeatherForecast>> InsertWeatherForecast()
        {
            var serviceResult = await _orderApiClient.InsertWeatherForecast(new Model.WeatherForecast { 
                Date = DateTime.Now,
                Summary = "Jamiryo-2",
                TemperatureC = new Random().Next(100, 200)
            });

            return serviceResult.Select(s => new WeatherForecast
            {
                Date = s.Date,
                Summary = s.Summary,
                TemperatureC = s.TemperatureC
            }).ToList();
        }

        [HttpGet("GenerateVeryLargeWeatherForecasts")]
        public async Task<List<WeatherForecast>> GenerateVeryLargeWeatherForecasts(int size)
        {
            var serviceResult = await _orderApiClient.GenerateVeryLargeWeatherForecasts(new Model.GenerateVeryLargeWeatherForecastRequest
            {
                Size = size > 0 ? size : 100
            });

            return serviceResult.Select(s => new WeatherForecast
            {
                Date = s.Date,
                Summary = s.Summary,
                TemperatureC = s.TemperatureC
            }).ToList();
        }
    }
}
