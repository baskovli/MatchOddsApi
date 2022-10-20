using MatchOdds.Domain.Interfaces;

namespace MatchOdds.Domain;

/// <summary>
/// IoC - wrap all services
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IMatchRepositoryService MatchRepositoryService { get; }
    IOddRepositoryService OddRepositoryServiceService { get; }
    /// <summary>
    /// Commits all changes
    /// </summary>
    void Commit();
    /// <summary>
    /// Discards all changes that has not been commited
    /// </summary>
    void RejectChanges();
}
