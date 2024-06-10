using Northwind.API.Domain.Entities;
using Northwind.API.Infrastructure.Data;
using Northwind.API.Interfaces;
using Northwind.Shared.EntityFrameworkCore.Domain.Repositories;

namespace Northwind.API.Infrastructure.Repositories;

public class ProductRepository : EntityFrameworkCoreRepository<Product>, IProductRepository
{
    public ProductRepository(NorthwindContext dbContext) 
        : base(dbContext)
    {
    }
}
