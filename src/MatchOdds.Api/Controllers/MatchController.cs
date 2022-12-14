using MatchOdds.Api.Controllers;
using MatchOdds.Domain;
using MatchOdds.Domain.Interfaces;
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
        //private readonly IMatchRepositoryService _matchRepositoryClient;
        //private readonly IOddRepositoryService _oddRepositoryService;      

        public MatchController(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            //_matchRepositoryClient = matchRepositoryClient;
            //_oddRepositoryService = oddRepositoryService;
        }

        // GET: api/match
        [HttpGet]
        public async Task<ActionResult<IList<MatchModel>>> Get()
        {
            try
            {
                var matches = await _unitOfWork.MatchRepositoryService.GetAllMatches();
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // GET api/match/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchModel>> Get(int id)
        {
            try
            {
                var match = await _unitOfWork.MatchRepositoryService.GetMatchById(id);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/match/1
        [HttpGet("teama/{title}")]
        public async Task<ActionResult<MatchModel>> Get(string title)
        {
            try
            {
                var match = await _unitOfWork.MatchRepositoryService.GetMatchByTeamAName(title);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                var match = await _unitOfWork.MatchRepositoryService.AddMatch(model);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                var match = await _unitOfWork.MatchRepositoryService.GetMatchById(id);
                if (match == null)
                {
                    return NotFound();
                }

                if (model.ID == 0)
                {
                    model.ID = id;
                }

                var updatedMatch = await _unitOfWork.MatchRepositoryService.UpdateMatch(model);
                return Ok(updatedMatch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/match/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var match = await _unitOfWork.MatchRepositoryService.GetMatchById(id);
                if (match == null)
                {
                    return NotFound();
                }

                var deleted = await _unitOfWork.MatchRepositoryService.DeleteMatch(id);
                if (deleted)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}