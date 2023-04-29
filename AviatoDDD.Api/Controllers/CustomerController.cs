using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.Services;
using AviatoDDD.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AviatoDDD.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();

        return Ok(customers);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var customer = await _customerService.GetOneAsync(id);

        return Ok(customer);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddCustomerRequestDTO dto)
    {
        var created = await _customerService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] AddCustomerRequestDTO dto, [FromRoute] Guid id)
    {
        var updated = await _customerService.UpdateAsync(id, dto);

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _customerService.DeleteAsync(id);

        return Ok(deleted);
    }
}