using MatchOdds.Data.Interfaces;

namespace MatchOdds.Data.UnitOfWork;

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
