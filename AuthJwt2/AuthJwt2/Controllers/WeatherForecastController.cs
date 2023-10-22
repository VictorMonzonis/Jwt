using AuthJwt2.Domain.Entities;
using AuthJwt2.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwt2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly JwtProvider _jwtProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JwtProvider jwtProvider)
        {
            _logger = logger;
            _jwtProvider = jwtProvider;
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginMember([FromBody] Member request)
        {
            // Get Member, check password hash...

            // Generate Jwt
            var token = _jwtProvider.Generate(request);
            return Ok(token);
        }
    }
}