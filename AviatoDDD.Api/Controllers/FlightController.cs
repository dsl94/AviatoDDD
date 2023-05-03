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
    private readonly IBookingService _bookingService;

    public FlightController(IFlightService flightService, IBookingService bookingService)
    {
        _flightService = flightService;
        _bookingService = bookingService;
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
    
    [HttpGet]
    [Route("{id:guid}/bookings")]
    public async Task<IActionResult> GetBookings([FromRoute] Guid id)
    {
        var bookings = await _bookingService.GetAllForFlightAsync(id);

        return Ok(bookings);
    }
    
    [HttpGet]
    [Route("{id:guid}/load")]
    public async Task<IActionResult> GetLoad([FromRoute] Guid id)
    {
        var load = await _flightService.GetLoadAsync(id);

        return Ok(load);
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