using MatchOdds.Domain.Models.Match;
using MatchOdds.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchOdds.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMatchRepositoryService _matchRepository;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(IMatchRepositoryService matchRepository)
        {
            _matchRepository = matchRepository;
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IList<MatchModel>> Get()
        {
            var matches = await _matchRepository.GetAllMatches();
            return matches;
        }
    }
}