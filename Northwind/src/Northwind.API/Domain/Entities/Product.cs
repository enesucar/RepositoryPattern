using Northwind.Shared.Domain.Entities;

namespace Northwind.API.Domain.Entities;

public class Product : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }
}
