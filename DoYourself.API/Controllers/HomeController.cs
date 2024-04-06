using DoYourself.Core.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DoYourself.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class WeatherForecast
        { 
            public int Id { get; set; }
            public string Name { get; set; }
            public string TemperatureCelsius { get; set; }
            public string? Summary { get; set; }
        }

        [HttpGet("task")]
        public ActionResult Get()
        {
            var weatherForecast = new[]
            {
                new WeatherForecast {
                Id = 1,
                Name = "Gar",
                TemperatureCelsius = "gar",
                Summary = "Hot" },
                new WeatherForecast {
                Id = 2,
                Name = "Gar",
                TemperatureCelsius = "gar",
                Summary = "Hot" },
            };
            
            return Ok(weatherForecast);
        }
    }
}
