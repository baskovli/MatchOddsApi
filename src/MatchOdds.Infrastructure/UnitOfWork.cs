using MatchOdds.Domain;
using MatchOdds.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchOdds.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly MatchOddsContext _dbContext;
    public IMatchRepositoryService MatchRepositoryService { get; }

    public IOddRepositoryService OddRepositoryServiceService { get; }

    public UnitOfWork(MatchOddsContext dbContext, IMatchRepositoryService matchRepository, IOddRepositoryService matchOddRepositoryService)
    {
        _dbContext = dbContext;
        MatchRepositoryService = matchRepository;
        OddRepositoryServiceService = matchOddRepositoryService;
    }

    public void Commit() => _dbContext.SaveChanges();

    public void RejectChanges()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries()
               .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }

    public void Dispose() => _dbContext.Dispose();

}

