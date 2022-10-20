using MatchOdds.Domain.Interfaces;

namespace MatchOdds.Domain.UnitOfWork;

/// <summary>
/// IoC - wrap all services
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IMatchRepositoryService MatchRepositoryService { get; }
    IOddRepositoryService OddRepositoryServiceService { get; }
}
