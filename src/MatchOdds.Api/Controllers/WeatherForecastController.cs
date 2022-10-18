using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchOdds.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IList<MatchModel>> Get()
        {
            var matches = await unitOfWork.Match.GetAllMatches();
            return matches;
        }
    }
}