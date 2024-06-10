using Northwind.Shared.Domain.Entities;

namespace Northwind.Shared.Domain.Repositories;

public interface IRepository<TEntity> :
    IReadRepository<TEntity>,
    IWriteRepository<TEntity>,
    IPersistRepository,
    IQuery<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> WithDetails();
}
