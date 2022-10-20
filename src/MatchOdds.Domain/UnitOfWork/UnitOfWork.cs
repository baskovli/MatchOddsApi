using MatchOdds.Domain.Interfaces;

namespace MatchOdds.Domain.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IMatchRepositoryService MatchRepositoryService { get; }

    public IOddRepositoryService OddRepositoryServiceService { get; }

    public UnitOfWork(IMatchRepositoryService matchRepository, IOddRepositoryService matchOddRepositoryService)
    {
        MatchRepositoryService = matchRepository;
        OddRepositoryServiceService = matchOddRepositoryService;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

