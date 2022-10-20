using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Repositories;

namespace MatchOdds.Infrastructure.Repositories;

public class MatchRepository : GenericRepository<Match, MatchOddsContext>, IMatchRepository
{
    public MatchRepository(MatchOddsContext context) : base(context)
    {

    }
}
