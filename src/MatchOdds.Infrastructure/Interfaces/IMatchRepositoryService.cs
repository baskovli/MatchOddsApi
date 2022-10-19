using MatchOdds.Domain.Models.Match;

namespace MatchOdds.Infrastructure.Interfaces;

public interface IMatchRepositoryService
{
    Task<IList<MatchModel>> GetAllMatches();
    Task<MatchModel> GetMatchById(int id);
    Task<MatchModel> AddMatch(MatchAddModel model);
    Task<MatchModel> UpdateMatch(MatchUpdateModel model);
    Task<bool> DeleteMatch(int id);
}
