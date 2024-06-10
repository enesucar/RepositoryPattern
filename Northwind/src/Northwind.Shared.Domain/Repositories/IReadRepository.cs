using Northwind.Shared.Domain.Entities;
using System.Linq.Expressions;

namespace Northwind.Shared.Domain.Repositories;

public interface IReadRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<List<TEntity>> GetListAsync(
       Expression<Func<TEntity, bool>> predicate,
       bool includeDetails = false,
       CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> predicate,
        string sorting,
        int page = 1,
        int size = 10,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetPaginatedListAsync(
        string sorting,
        int page = 1,
        int size = 10,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}
