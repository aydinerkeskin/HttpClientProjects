using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Model
{
    public class GenerateVeryLargeWeatherForecastRequest
    {
        public int Size { get; set; } = 100;
    }
}
