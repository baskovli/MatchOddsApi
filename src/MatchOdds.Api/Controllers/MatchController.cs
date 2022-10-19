using MatchOdds.Api.Controllers;
using MatchOdds.Infrastructure.Interfaces;
using MatchOdds.Domain.Models.Match;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MatchOdds.Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class MatchController : BaseController
    {
        private readonly IMatchRepositoryService _matchRepositoryClient;
        private readonly IOddRepositoryService _oddRepositoryService;

        public MatchController(IMatchRepositoryService matchRepositoryClient, IOddRepositoryService oddRepositoryService)
        {
            _matchRepositoryClient = matchRepositoryClient;
            _oddRepositoryService = oddRepositoryService;
        }

        // GET: api/match
        [HttpGet]
        public async Task<ActionResult<IList<MatchModel>>> Get()
        {
            try
            {
                var matches = await _matchRepositoryClient.GetAllMatches();
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // GET api/match/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchModel>> Get(int id)
        {
            try
            {
                var match = await _matchRepositoryClient.GetMatchById(id);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/match/1
        [HttpGet("teama/{title}")]
        public async Task<ActionResult<MatchModel>> Get(string title)
        {
            try
            {
                var match = await _matchRepositoryClient.GetMatchByTeamAName(title);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/match
        [HttpPost]
        public async Task<ActionResult<MatchModel>> Post([FromBody] MatchAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var match = await _matchRepositoryClient.AddMatch(model);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/match/1
        [HttpPut("{id}")]
        public async Task<ActionResult<MatchModel>> Put(int id, [FromBody] MatchUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var match = await _matchRepositoryClient.GetMatchById(id);
                if (match == null)
                {
                    return NotFound();
                }

                if (model.ID == 0)
                {
                    model.ID = id;
                }

                var updatedMatch = await _matchRepositoryClient.UpdateMatch(model);
                return Ok(updatedMatch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/match/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var match = await _matchRepositoryClient.GetMatchById(id);
                if (match == null)
                {
                    return NotFound();
                }

                var deleted = await _matchRepositoryClient.DeleteMatch(id);
                if (deleted)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}