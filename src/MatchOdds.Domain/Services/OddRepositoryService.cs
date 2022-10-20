using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Interfaces;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;
using MatchOdds.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace MatchOdds.Domain.Services
{
    public class OddRepositoryService : RepositoryService, IOddRepositoryService
    {
        private readonly IOddRepository _matchOddRepository;

        private readonly string cacheKey = "MatchesOdds";
        public OddRepositoryService(IOddRepository matchOddRepository, IMapper mapper, IMemoryCache cache) : base(mapper, cache)
        {
            _matchOddRepository = matchOddRepository;
        }

        public async Task<IList<OddModel>> GetAllMatchOdds()
        {
            var cachedMatchesOdds = await _memoryCache.GetOrCreateAsync<IList<OddModel>>(cacheKey, async (c) =>
            {
                var matchesOdds = _matchOddRepository.FindAll(x => x.Match).ToList();

                if (matchesOdds.Any())
                {
                    c.SlidingExpiration = TimeSpan.FromMinutes(defaultSlideExpirationInMinutes);
                    return _mapper.Map<IList<OddModel>>(matchesOdds);
                }
                return default;
            });
            return cachedMatchesOdds;
        }

        public async Task<OddModel> GetMatchOddById(int id)
        {
            var cachedMatchesOdds = _memoryCache.Get<IList<OddModel>>(cacheKey);
            var cachedMatchOdd = cachedMatchesOdds?.FirstOrDefault(m => m.ID == id);

            if (cachedMatchOdd == null)
            {
                var match = await _matchOddRepository.GetByIdAsync(id, x => x.Match);
                if (match != null)
                {
                    var mappedMatchOdd = _mapper.Map<OddModel>(match);

                    if (cachedMatchesOdds == null)
                    {
                        cachedMatchesOdds = new List<OddModel>();
                    }

                    cachedMatchesOdds.Add(mappedMatchOdd);

                    _memoryCache.Set(cacheKey, mappedMatchOdd, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(defaultSlideExpirationInMinutes)));
                    return mappedMatchOdd;
                }
                return null;
            }

            return cachedMatchOdd;
        }

        public async Task<OddModel> AddMatchOdd(OddAddModel model)
        {
            var matchOddMapped = _mapper.Map<Odd>(model);
            var matchOdd = await _matchOddRepository.CreateAsync(matchOddMapped);

            // Update Cache with new match
            // return await GetMatchById(match.ID);

            // Reset Memory Cache
            _memoryCache.Remove(cacheKey);

            return _mapper.Map<OddModel>(matchOdd);
        }

        public async Task<OddModel> UpdateMatchOdd(OddUpdateModel model)
        {
            var matchOddMapped = _mapper.Map<Odd>(model);
            var matchOdd = await _matchOddRepository.UpdateAsync(matchOddMapped);

            // Reset Memory Cache
            _memoryCache.Remove(cacheKey);

            return _mapper.Map<OddModel>(matchOdd);
        }

        public async Task<bool> DeleteMatchOdd(int id)
        {
            return await _matchOddRepository.DeleteAsync(id);
        }

    }
}