using MatchOdds.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MatchOdds.Infrastructure;

public abstract class GenericRepository<T, C> : IGenericRepository<T>
    where T : class
    where C : DbContext
{
    internal readonly C _context;
    internal DbSet<T> dbSet;
    public GenericRepository(C context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public IQueryable<T> FindAll(params Expression<Func<T, object>>[] propertiesToInclude)
    {
        if (propertiesToInclude.Length > 0)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var expression in propertiesToInclude)
            {
                query = query.Include(expression);
            }

            return query.AsNoTracking();
        }
        return dbSet.AsNoTracking();
    }

    //public T GetById(int id)
    //{
    //    var aa = FindByCondition(x => x == 5);
    //  return _context.Set<T>().Find(id);
    //}

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return dbSet.Where(expression).AsNoTracking();
    }
    public async Task<T> CreateAsync(T entity)
    {
        dbSet.Add(entity);
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
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToInclude)
    {
        if (propertiesToInclude.Length > 0)
        {
            var keyProperty = _context.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];

            if (keyProperty == null)
            {
                return default;
            }

            var query = dbSet.AsNoTracking().AsQueryable();

            foreach (var expression in propertiesToInclude)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync(s => EF.Property<int>(s, keyProperty.Name) == id);
        }
        return await dbSet.FindAsync(id);
    }

    //public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] propertiesToInclude)
    //{
    //    if (propertiesToInclude.Length > 0)
    //    {
    //        var query = _context.Set<T>().AsQueryable();
    //        foreach (var expression in propertiesToInclude)
    //        {
    //            query = query.Include(expression);
    //        }

    //        return await query.ToListAsync();
    //    }
    //    return await _context.Set<T>().ToListAsync();

    //}

    //public async Task<T> AddAsync(T entity)
    //{
    //    _context.Set<T>().Add(entity);
    //    await _context.SaveChangesAsync();
    //    return entity;
    //}

    //public async Task<T> UpdateAsync(T entity)
    //{
    //    _context.Entry(entity).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //    return entity;
    //}

    //public async Task<bool> DeleteAsync(int id)
    //{
    //    var entity = await _context.Set<T>().FindAsync(id);
    //    if (entity == null)
    //    {
    //        return false;
    //    }

    //    _context.Set<T>().Remove(entity);
    //    await _context.SaveChangesAsync();

    //    return true;
    //}

}