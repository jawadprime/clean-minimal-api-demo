using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Common;

public class Repository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _set;

    public Repository(AppDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll(FindOptions? options = default)
        => ApplyOptions(options);

    public IQueryable<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        FindOptions? options = default)
        => ApplyOptions(options).Where(predicate);

    public async Task<TEntity?> FindOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        FindOptions? options = default)
        => await ApplyOptions(options).FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _set.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var tracked = _context.ChangeTracker.Entries<TEntity>()
                      .FirstOrDefault(e => e.Entity == entity);

        if (tracked == null)
            _context.Attach(entity);

        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _set.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        await _set.Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => _set.AnyAsync(predicate, cancellationToken);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => _set.CountAsync(predicate, cancellationToken);

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