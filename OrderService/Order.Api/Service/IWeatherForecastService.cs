using System.Collections.Generic;

namespace Order.Api.Service
{
    public interface IWeatherForecastService
    {
        public bool Add(WeatherForecast weatherForecast);

        public List<WeatherForecast> GetList();
    }
}
