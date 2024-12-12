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
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers()
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
    public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        var customerResponse = new CustomerResponse 
        {
            Id = customer.Id,
            Name = customer.Name,
        };

        return Ok(customerResponse);
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
    {
        var customer = await context.Customers.FindAsync(id);
        customer.Name = customerUpdateRequest.Name;
        await context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Customers
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(CustomerCreateRequest customerCreateRequest)
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        context.Customers.Remove(customer);
        await context.SaveChangesAsync();

        return NoContent();
    }
}