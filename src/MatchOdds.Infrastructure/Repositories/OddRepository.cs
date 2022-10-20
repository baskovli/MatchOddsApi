using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Repositories;

namespace MatchOdds.Infrastructure.Repositories;

public class OddRepository : GenericRepository<Odd, MatchOddsContext>, IOddRepository
{
    public OddRepository(MatchOddsContext context) : base(context) { }
}
