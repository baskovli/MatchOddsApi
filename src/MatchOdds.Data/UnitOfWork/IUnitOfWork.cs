using MatchOdds.Data.Interfaces;

namespace MatchOdds.Data.UnitOfWork;

/// <summary>
/// IoC - wrap all services
/// </summary>
public interface IUnitOfWork
{
    IMatchRepositoryService MatchRepositoryService { get; }
    IOddRepositoryService OddRepositoryServiceService { get; }
}
