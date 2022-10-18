using System.Linq.Expressions;

namespace MatchOdds.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] propertiesToInclude);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToInclude);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
