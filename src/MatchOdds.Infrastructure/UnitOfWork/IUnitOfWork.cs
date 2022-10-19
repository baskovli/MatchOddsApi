using MatchOdds.Infrastructure.Interfaces;

namespace MatchOdds.Infrastructure.UnitOfWork;

/// <summary>
/// IoC - wrap all services
/// </summary>
public interface IUnitOfWork
{
    IMatchRepositoryService MatchRepositoryService { get; }
    IOddRepositoryService OddRepositoryServiceService { get; }
}
