namespace MatchOdds.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMatchRepository Match
        {
            get;
        }
        IOddRepository Odd
        {
            get;
        }
        //Save method was included here because we need to save changes to the database regardless of which repository have been changed.
        int Save();
    }
}
