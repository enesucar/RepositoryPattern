namespace Northwind.Shared.Domain.Repositories;

public interface IPersistRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
