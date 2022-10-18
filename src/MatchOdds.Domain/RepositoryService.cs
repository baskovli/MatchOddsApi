using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace MatchOdds.Domain;

public abstract class RepositoryService
{
    public readonly IMapper _mapper;
    public readonly IMemoryCache _memoryCache;

    public int defaultSlideExpirationInMinutes = 30;

    public RepositoryService(IMapper mapper, IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
    }
}
