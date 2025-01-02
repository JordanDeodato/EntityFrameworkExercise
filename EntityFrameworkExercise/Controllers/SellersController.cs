using EntityFrameworkExercise.Data;
using EntityFrameworkExercise.Dto;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellersController(StoreContext context) : ControllerBase
{
    // GET: api/Sellers
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(SellerResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSellers()
    {
        var sellers = await context.Sellers
            .Select(seller => new SellerResponse 
            { 
                Id = seller.Id, Name = seller.Name 
            }).ToListAsync();

        return Ok(sellers);
    }

    // GET: api/Sellers/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(SellerResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSeller(int id)
    {
        var seller = await context.Sellers.SingleAsync(s => s.Id == id);

        if(seller == null)
        {
            return NotFound();
        }

        var sellerResponse = new SellerResponse
        {
            Id = seller.Id,
            Name = seller.Name
        };

        return Ok(sellerResponse);
    }

    // PUT: api/Sellers/5
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutSeller(int id, SellerUpdateRequest sellerUpdateRequest)
    {
        var seller = await context.Sellers.SingleAsync(s => s.Id == id);
        seller.Name = sellerUpdateRequest.Name;
        await context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Sellers
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> PostSeller(SellerCreateRequest sellerCreateRequest)
    {
        var seller = new Seller
        {
            Name = sellerCreateRequest.Name
        };

        context.Sellers.Add(seller);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSeller), new { id = seller.Id }, seller);
    }

    // DELETE: api/Sellers/5
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteSeller(int id)
    {
        var seller = await context.Sellers.FirstOrDefaultAsync(s => s.Id == id);

        if(seller == null)
        {
            return NotFound();
        }

        context.Sellers.Remove(seller);
        await context.SaveChangesAsync();

        return NoContent();
    }
}