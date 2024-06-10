using Northwind.Shared.Domain.Entities;
using System.Linq.Expressions;

namespace Northwind.Shared.Domain.Repositories;

public interface IWriteRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task InsertManyAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateManyAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task DeleteManyAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
}
