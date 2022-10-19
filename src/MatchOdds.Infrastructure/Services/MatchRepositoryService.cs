using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Repositories;
using MatchOdds.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MatchOdds.Infrastructure.Services;

public class MatchRepositoryService : RepositoryService, IMatchRepositoryService
{
    private readonly IMatchRepository _matchRepository;

    private const string cacheKey = "matches";
    public MatchRepositoryService(IMatchRepository matchRepository, IMapper mapper, IMemoryCache cache) : base(mapper, cache)
    {
        _matchRepository = matchRepository;
    }

    public async Task<IList<MatchModel>> GetAllMatches()
    {
        var cachedMatches = await _memoryCache.GetOrCreateAsync<IList<MatchModel>>(cacheKey, async (c) =>
        {
            var matches = _matchRepository.FindAll(x => x.Odds).ToList();

            if (matches.Any())
            {
                c.SlidingExpiration = TimeSpan.FromMinutes(defaultSlideExpirationInMinutes);
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
    public async Task<MatchModel?> GetMatchById(int id)
    {
        var cachedMatches = _memoryCache.Get<IList<MatchModel>>(cacheKey);
        var cachedMatch = cachedMatches?.FirstOrDefault(m => m.ID == id);

        if (cachedMatch == null)
        {
            var match = _matchRepository.FindByCondition(x => x.ID == id).FirstOrDefault();
            if (match != null)
            {
                var mappedMatch = _mapper.Map<MatchModel>(match);

                if (cachedMatches == null)
                {
                    cachedMatches = new List<MatchModel>();
                }

                cachedMatches.Add(mappedMatch);

                _memoryCache.Set(cacheKey, cachedMatches, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(defaultSlideExpirationInMinutes)));
                return mappedMatch;
            }
            return default;
        }

        return cachedMatch;
    }

    /// <summary>
    /// Get match by team A name
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<MatchModel?> GetMatchByTeamAName(string teamName)
    {
        var cachedMatches = _memoryCache.Get<IList<MatchModel>>(cacheKey);
        var cachedMatch = cachedMatches?.FirstOrDefault(m => m.TeamA == teamName);

        if (cachedMatch == null)
        {
            var match = await _matchRepository.FindByCondition(x => x.TeamA == teamName).FirstOrDefaultAsync();
            if (match != null)
            {
                var mappedMatch = _mapper.Map<MatchModel>(match);

                cachedMatches ??= new List<MatchModel>();

                cachedMatches.Add(mappedMatch);

                _memoryCache.Set(cacheKey, cachedMatches, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(defaultSlideExpirationInMinutes)));
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
        var match = await _matchRepository.CreateAsync(matchMapped);
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
        var match = await _matchRepository.UpdateAsync(matchMapped);

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
        return await _matchRepository.DeleteAsync(id);
    }

}