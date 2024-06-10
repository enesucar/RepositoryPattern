using Microsoft.EntityFrameworkCore;
using Northwind.API.Domain.Entities;

namespace Northwind.API.Infrastructure.Data;

public class NorthwindContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("NorthwindDb");
    }
}
