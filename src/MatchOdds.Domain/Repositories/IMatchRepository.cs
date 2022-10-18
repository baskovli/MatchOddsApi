using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;

namespace MatchOdds.Domain.Repositories
{
    public interface IMatchRepository : IGenericRepository<Match>
    {
        Task<IList<MatchModel>> GetAllMatches();
        Task<MatchModel> GetMatchById(int id);
        Task<MatchModel> AddMatch(MatchAddModel model);
        Task<MatchModel> UpdateMatch(MatchUpdateModel model);
        Task<bool> DeleteMatch(int id);
        Task<MatchModel> GetMatchesByTeamA(string teamA);
    }
}
