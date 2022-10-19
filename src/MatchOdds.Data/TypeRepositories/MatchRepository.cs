using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MatchOdds.Data.TypeRepositories;

public class MatchRepository : GenericRepository<Match, MatchOddsContext>, IMatchRepository
{
    public MatchRepository(MatchOddsContext context) : base(context)
    {

    }
}
