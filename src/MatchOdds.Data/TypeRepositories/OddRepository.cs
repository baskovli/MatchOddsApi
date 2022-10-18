using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Repositories;

namespace MatchOdds.Data.TypeRepositories;

public class OddRepository : GenericRepository<Odd, MatchOddsContext>, IOddRepository
{
    public OddRepository(MatchOddsContext context) : base(context) { }
}
