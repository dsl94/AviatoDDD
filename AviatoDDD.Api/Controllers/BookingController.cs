using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.DTO.Booking;
using AviatoDDD.Domain.Services;
using AviatoDDD.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AviatoDDD.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController: ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingService.GetAllAsync();

        return Ok(bookings);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var booking = await _bookingService.GetOneAsync(id);

        return Ok(booking);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] CreateBookingOfferDTO dto)
    {
        var created = await _bookingService.CreateBookingOfferAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
    }
    
    [HttpPut]
    [Route("{id:guid}/accept")]
    public async Task<IActionResult> AcceptOffer([FromRoute] Guid id)
    {
        var accepted = await _bookingService.AcceptBookingOfferAsync(id);

        return Ok(accepted);
    }
    
    [HttpPut]
    [Route("{id:guid}/decline")]
    public async Task<IActionResult> DeclineOffer([FromRoute] Guid id)
    {
        var declined = await _bookingService.DeclineBookingOfferAsync(id);

        return Ok(declined);
    }
}