using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.DTO.Flight;
using AviatoDDD.Domain.Services;
using AviatoDDD.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AviatoDDD.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController: ControllerBase
{
    private readonly IFlightService _flightService;
    
    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var flights = await _flightService.GetAllAsync();

        return Ok(flights);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var flight = await _flightService.GetOneAsync(id);

        return Ok(flight);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddFlightRequestDTO dto)
    {
        var created = await _flightService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] AddFlightRequestDTO dto, [FromRoute] Guid id)
    {
        var updated = await _flightService.UpdateAsync(id, dto);

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _flightService.DeleteAsync(id);

        return Ok(deleted);
    }
}