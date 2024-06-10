using Northwind.Shared.Domain.Entities;
using Northwind.Shared.Domain.Repositories;

namespace Northwind.Shared.EntityFrameworkCore.Domain.Repositories;

public interface IEntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
}