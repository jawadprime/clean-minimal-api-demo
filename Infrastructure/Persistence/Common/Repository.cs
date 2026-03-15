using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Common;

internal class Repository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _set;

    public Repository(AppDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll(FindOptions? options = null)
        => ApplyOptions(options);

    public IQueryable<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        FindOptions? options = null)
        => ApplyOptions(options).Where(predicate);

    public async Task<TEntity?> FindOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        FindOptions? options = null)
        => await ApplyOptions(options).FirstOrDefaultAsync(predicate);

    public async Task AddAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities)
    {
        await _set.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _set.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        await _set.Where(predicate).ExecuteDeleteAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => _set.AnyAsync(predicate);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        => _set.CountAsync(predicate);

    private IQueryable<TEntity> ApplyOptions(FindOptions? options)
    {
        options ??= new();

        IQueryable<TEntity> query = _set;

        if (options.IsIgnoreAutoIncludes)
            query = query.IgnoreAutoIncludes();

        if (options.IsAsNoTracking)
            query = query.AsNoTracking();

        return query;
    }
}