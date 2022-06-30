using System.Collections.Generic;
using System.Linq;

namespace Order.Api.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly object _lockObject = new object();
        private List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

        public bool Add(WeatherForecast weatherForecast)
        {
            lock (_lockObject)
            {
                if (!_weatherForecasts.Any(a => a.Date == weatherForecast.Date))
                {
                    _weatherForecasts.Add(weatherForecast);
                    return true;
                }
            }
            return false;
        }

        public List<WeatherForecast> GetList()
        {
            return _weatherForecasts;
        }
    }
}
