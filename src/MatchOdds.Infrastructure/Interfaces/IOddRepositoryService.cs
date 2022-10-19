using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;

namespace MatchOdds.Infrastructure.Interfaces;

public interface IOddRepositoryService
{
    Task<IList<OddModel>> GetAllMatchOdds();
    Task<OddModel> GetMatchOddById(int id);
    Task<OddModel> AddMatchOdd(OddAddModel model);
    Task<OddModel> UpdateMatchOdd(OddUpdateModel model);
    Task<bool> DeleteMatchOdd(int id);
}
