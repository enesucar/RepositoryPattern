namespace Northwind.API.Models;

public class ProductRequest
{
    public string Sorting { get; set; } = "Id ASC";

    public int Page { get; set; } = 1;

    public int Size { get; set; } = 10;
}
