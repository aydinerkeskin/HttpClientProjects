using Microsoft.AspNetCore.Mvc;

namespace HttpClientWithExtensions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public string Index()
        {
            return "WeatherController a bir bak hele!";
        }
    }
}
