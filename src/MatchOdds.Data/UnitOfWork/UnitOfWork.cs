using MatchOdds.Domain.Repositories;

namespace MatchOdds.Data.UnitOfWork;

#pragma warning disable
public class UnitOfWork : IUnitOfWork
{
    private MatchOddsContext context;

    public UnitOfWork(MatchOddsContext context)
    {
        this.context = context;
    }

    public IMatchRepository Match
    {
        get;
        private set;
    }

    public IOddRepository Odd
    {
        get;
        private set;
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public int Save()
    {
        return context.SaveChanges();
    }
}
