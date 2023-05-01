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
    
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] CreateBookingOfferDTO dto)
    {
        var created = await _bookingService.CreateBookingOffer(dto);

        // return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        return Ok(created);
    }
}