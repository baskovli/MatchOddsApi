using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Repositories;

namespace MatchOdds.Data.TypeRepositories;

public class MatchRepository : GenericRepository<Match, MatchOddsContext>, IMatchRepository
{
    public MatchRepository(MatchOddsContext context) : base(context) { }
}
