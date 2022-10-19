using MatchOdds.Infrastructure.Interfaces;

namespace MatchOdds.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IMatchRepositoryService MatchRepositoryService { get; }

    public IOddRepositoryService OddRepositoryServiceService { get; }

    public UnitOfWork(IMatchRepositoryService matchRepository, IOddRepositoryService matchOddRepositoryService)
    {
        MatchRepositoryService = matchRepository;
        OddRepositoryServiceService = matchOddRepositoryService;
    }
}
