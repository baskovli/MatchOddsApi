using System.Linq.Expressions;

namespace MatchOdds.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] propertiesToInclude);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToInclude);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
