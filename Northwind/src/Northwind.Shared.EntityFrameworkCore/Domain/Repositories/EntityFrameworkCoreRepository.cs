using Microsoft.EntityFrameworkCore;
using Northwind.Shared.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Northwind.Shared.EntityFrameworkCore.Domain.Repositories;

public abstract class EntityFrameworkCoreRepository<TEntity> : 
    IEntityFrameworkCoreRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly DbContext DbContext;

    public EntityFrameworkCoreRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<List<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails ? WithDetails() : Query();
        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetListAsync(
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails ? WithDetails() : Query();
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> predicate,
        string sorting,
        int page = 1,
        int size = 10,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails ? WithDetails() : Query();
        return await query.Where(predicate).OrderBy(sorting).Page(page, size).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetPaginatedListAsync(
        string sorting,
        int page = 1,
        int size = 10,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails ? WithDetails() : Query();
        return await query.OrderBy(sorting).Page(page, size).ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails ? WithDetails() : Query();
        return await query.Where(predicate).FirstOrDefaultAsync(cancellationToken); 
    }

    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return await FindAsync(predicate, includeDetails, cancellationToken) ?? throw new Exception("Not found exception");
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Query().CountAsync(predicate, cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await Query().CountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Query().AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<TEntity> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        return (await DbContext.AddAsync(entity, cancellationToken)).Entity;
    }

    public virtual async Task InsertManyAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        await DbContext.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        DbContext.Update(entity);
        return await Task.FromResult(entity);
    }

    public virtual Task UpdateManyAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        DbContext.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        DbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var entities = await GetListAsync(predicate, cancellationToken: cancellationToken);
        await DeleteManyAsync(entities, cancellationToken);
    }

    public virtual Task DeleteManyAsync(
       IEnumerable<TEntity> entities,
       CancellationToken cancellationToken = default)
    {
        DbContext.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual IQueryable<TEntity> Query()
    {
        return DbContext.Set<TEntity>().AsQueryable();
    }

    public virtual IQueryable<TEntity> WithDetails()
    {
        return Query();
    }
}
