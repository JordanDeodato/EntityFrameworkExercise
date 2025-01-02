using EntityFrameworkExercise.Data;
using EntityFrameworkExercise.Dto;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(StoreContext context) : ControllerBase
{
    // GET: api/Products
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ProductResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetProducts()
    {
        var products = await context.Products
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            }).ToListAsync();

        return Ok(products);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ProductResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetProduct(int id)
    {
        var product = await context.Products.SingleAsync(p => p.Id == id);

        if (product == null) 
        {
            return NotFound();
        }

        var productResponse = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };

        return Ok(productResponse);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutProduct(int id, ProductUpdateRequest productUpdateRequest)
    {
        var product = await context.Products.SingleAsync(p => p.Id == id);

        if(product == null)
        {
            return NotFound();
        }

        product.Name = productUpdateRequest.Name;
        product.Price = productUpdateRequest.Price;

        await context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Products
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> PostProduct(ProductCreateRequest productCreateRequest)
    {
        var product = new Product
        {
            Name = productCreateRequest.Name,
            Price = productCreateRequest.Price,
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return Ok(product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if( product == null)
        {
            return NotFound();
        }

        context.Products.Remove(product);

        await context.SaveChangesAsync();

        return NoContent();
    }
}