using MatchOdds.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MatchOdds.Data;

public abstract class GenericRepository<T, C> : IGenericRepository<T> 
    where T : class
    where C : DbContext
{
    protected readonly C _context;
    public GenericRepository(C context)
    {
        _context = context;
    }
    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] propertiesToInclude)
    {
        if (propertiesToInclude.Length > 0)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var expression in propertiesToInclude)
            {
                query = query.Include(expression);
            }

            return await query.ToListAsync();
        }
        return await _context.Set<T>().ToListAsync();

    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToInclude)
    {
        if (propertiesToInclude.Length > 0)
        {
            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            foreach (var expression in propertiesToInclude)
            {
                query = query.Include(expression);
            }
            return await query.FirstOrDefaultAsync(s => EF.Property<int>(s, keyProperty.Name) == id);
        }
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

}