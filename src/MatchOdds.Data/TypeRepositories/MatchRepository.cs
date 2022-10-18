using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MatchOdds.Data.TypeRepositories;

public class MatchRepository : GenericRepository<Match, MatchOddsContext>, IMatchRepository
{
    public readonly IMapper _mapper;
    public readonly IMemoryCache _memoryCache;
    private const string cacheKey = "matches";

    public MatchRepository(MatchOddsContext context, IMapper mapper, IMemoryCache memoryCache) : base(context)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<IList<MatchModel>> GetAllMatches()
    {
        var cachedMatches = await _memoryCache.GetOrCreateAsync<IList<MatchModel>>(cacheKey, async (c) =>
        {
            var matches = await GetAllAsync();

            if (matches.Any())
            {
                c.SlidingExpiration = TimeSpan.FromMinutes(30/*defaultSlideExpirationInMinutes*/);
                return _mapper.Map<IList<MatchModel>>(matches); ;
            }
            return default;
        });
        return cachedMatches;
    }

    /// <summary>
    /// Get match by spesific id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<MatchModel> GetMatchById(int id)
    {
        var cachedMatches = _memoryCache.Get<IList<MatchModel>>(cacheKey);
        var cachedMatch = cachedMatches?.FirstOrDefault(m => m.ID == id);

        if (cachedMatch == null)
        {
            //var match = await _matchRepository.GetByIdAsync(id, x => x.Odds);
            var match = await GetByIdAsync(id, x => x.Odds);
            if (match != null)
            {
                var mappedMatch = _mapper.Map<MatchModel>(match);

                if (cachedMatches == null) cachedMatches = new List<MatchModel>();
                cachedMatches.Add(mappedMatch);

                _memoryCache.Set(cacheKey, cachedMatches, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                return mappedMatch;
            }
            return default;
        }

        return cachedMatch;
    }

    /// <summary>
    /// Create new match
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<MatchModel> AddMatch(MatchAddModel model)
    {
        var matchMapped = _mapper.Map<Match>(model);
        //var match = await _matchRepository.AddAsync(matchMapped);
        var match = await AddAsync(matchMapped);
        // Reset Memory Cache
        _memoryCache.Remove(cacheKey);

        return _mapper.Map<MatchModel>(match);
    }

    /// <summary>
    /// Update exsisting match
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<MatchModel> UpdateMatch(MatchUpdateModel model)
    {
        var matchMapped = _mapper.Map<Match>(model);
        //var match = await _matchRepository.UpdateAsync(matchMapped);
        var match = await UpdateAsync(matchMapped);

        // Reset Memory Cache
        _memoryCache.Remove(cacheKey);

        return _mapper.Map<MatchModel>(match);
    }

    /// <summary>
    /// Delete match
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteMatch(int id)
    {
        //return await _matchRepository.DeleteAsync(id);
        return await DeleteAsync(id);
    }

    public async Task<MatchModel> GetMatchesByTeamA(string teamA)
    {
        var match = await _context.Match.Where(x => x.TeamA == teamA).FirstOrDefaultAsync();
        return _mapper.Map<MatchModel>(match);
    }
}
