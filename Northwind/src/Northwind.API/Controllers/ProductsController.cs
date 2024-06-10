using Microsoft.AspNetCore.Mvc;
using Northwind.API.Domain.Entities;
using Northwind.API.Infrastructure.Data;
using Northwind.API.Interfaces;
using Northwind.API.Models;

namespace Northwind.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedListAsync([FromQuery] ProductRequest request)
    {
        var product = await _productRepository.GetPaginatedListAsync(
            request.Sorting, 
            request.Page, 
            request.Size);
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var product = await _productRepository.FindAsync(o => o.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Product product)
    {
        var createdProduct = await _productRepository.InsertAsync(product);
        await _productRepository.SaveChangesAsync();
        return Created("", createdProduct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Product product)
    {
        if (!(await _productRepository.AnyAsync(o => o.Id == product.Id)))
        {
            return NotFound();
        }

        var updatedProduct = await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        if (!(await _productRepository.AnyAsync(o => o.Id == id)))
        {
            return NotFound();
        }

        await _productRepository.DeleteAsync(o => o.Id == id);
        await _productRepository.SaveChangesAsync();
        return NoContent();
    }
}
