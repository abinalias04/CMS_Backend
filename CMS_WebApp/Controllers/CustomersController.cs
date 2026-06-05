using Microsoft.AspNetCore.Mvc;
using WebApp.Entity.Models;
using WebApp.Services.Repository;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _repository;

    public CustomersController(
        ICustomerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers =
            await _repository.GetAllAsync();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer =
            await _repository.GetByIdAsync(id);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Customer customer)
    {
        if (await _repository.EmailExistsAsync(customer.Email))
        {
            return BadRequest(
                "Email already exists");
        }

        customer.CustomerId = Guid.NewGuid();
        customer.CreatedDate = DateTime.UtcNow;
        customer.ModifiedDate = DateTime.UtcNow;

        var result =
            await _repository.CreateAsync(customer);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        Customer customer)
    {
        var existing =
            await _repository.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        existing.FirstName = customer.FirstName;
        existing.LastName = customer.LastName;
        existing.Email = customer.Email;
        existing.PhoneNumber = customer.PhoneNumber;
        existing.Address = customer.Address;
        existing.ModifiedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted =
            await _repository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok();
    }
}