using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkExercise.Data;
using EntityFrameworkExercise.Models;
using EntityFrameworkExercise.Dto;

namespace EntityFrameworkExercise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController(StoreContext context) : ControllerBase
{
    // GET: api/Sales
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(SaleResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSales()
    {
        var sales = await context.Sales.
        Select(sale => new SaleResponse 
        { 
            Id = sale.Id,
            Date = sale.Date,
            SellerId = sale.SellerId,
            CustomerId = sale.CustomerId
        }).ToListAsync();

        return Ok(sales);
    }

    // GET: api/Sales/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(SaleResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSale(int id)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.Id == id);
            
        if(sale == null)
        {
            return NotFound();
        }

        var saleReponse = new SaleResponse
        {
            Id = sale.Id,
            Date = sale.Date,
            SellerId = sale.SellerId,
            CustomerId = sale.CustomerId
        };

        return Ok(saleReponse);
    }

    // PUT: api/Sales/5
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutSale(int id, SaleUpdateRequest saleUpdateRequest)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.Id == id);
        if(sale == null)
        {
            return NotFound();
        }

        sale.Date = saleUpdateRequest.Date;
        sale.SellerId = saleUpdateRequest.SellerId;
        sale.CustomerId = saleUpdateRequest.CustomerId;

        await context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Sales
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> PostSale(SaleCreateRequest saleCreateRequest)
    {
        var sale = new Sale
        {
            Date = saleCreateRequest.Date,
            SellerId = saleCreateRequest.SellerId,
            CustomerId = saleCreateRequest.CustomerId
        };

        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Sales/5
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.Id == id);
        if(sale == null) 
        { 
            return NotFound(); 
        }

        context.Remove(sale);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
