namespace Northwind.Shared.Domain.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
