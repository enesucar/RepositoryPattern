using Northwind.API.Domain.Entities;
using Northwind.Shared.EntityFrameworkCore.Domain.Repositories;

namespace Northwind.API.Interfaces;

public interface IProductRepository : IEntityFrameworkCoreRepository<Product>
{
}
