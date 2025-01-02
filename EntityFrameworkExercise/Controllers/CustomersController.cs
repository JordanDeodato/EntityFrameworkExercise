using EntityFrameworkExercise.Data;
using EntityFrameworkExercise.Dto;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(StoreContext context) : ControllerBase
{
    // GET: api/Customers
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetCustomers()
    {
        var customers = await context.Customers
            .Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
            })
            .ToListAsync();

        return Ok(customers);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetCustomer(int id)
    {
        var customer = await context.Customers.SingleAsync(c => c.Id == id);

        if(customer == null) 
        {
            return NotFound();
        }

        var customerResponse = new CustomerResponse 
        {
            Id = customer.Id,
            Name = customer.Name,
        };

        return Ok(customerResponse);
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
    {
        var customer = await context.Customers.SingleAsync(c => c.Id == id);
        customer.Name = customerUpdateRequest.Name;
        await context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Customers
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> PostCustomer(CustomerCreateRequest customerCreateRequest)
    {
        var customer = new Customer
        {
            Name = customerCreateRequest.Name
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if(customer == null)
        {
            return NotFound();
        }

        context.Customers.Remove(customer);

        await context.SaveChangesAsync();

        return NoContent();
    }
}