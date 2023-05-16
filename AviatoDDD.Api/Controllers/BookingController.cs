using AviatoDDD.Commands;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.DTO.Booking;
using AviatoDDD.Domain.Services;
using AviatoDDD.Filters;
using AviatoDDD.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AviatoDDD.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController: ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _mediator.Send(new GetAllBookingsQuery());

        return Ok(bookings);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var query = new GetBookingByIdQuery
        {
            Id = id
        };
        var booking = await _mediator.Send(query);

        return Ok(booking);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] CreateBookingOfferDTO dto)
    {
        var command = new CreateBookingOfferCommand()
        {
            BookingOffer = dto
        };
        await _mediator.Publish(command);

        return Ok();
    }
    
    [HttpPut]
    [Route("{id:guid}/accept")]
    public async Task<IActionResult> AcceptOffer([FromRoute] Guid id)
    {
        var command = new AcceptBookingCommand()
        {
            Id = id
        };

        await _mediator.Publish(command);

        return Ok();
    }
    
    [HttpPut]
    [Route("{id:guid}/decline")]
    public async Task<IActionResult> DeclineOffer([FromRoute] Guid id)
    {
        var command = new DeclineBookingCommand()
        {
            Id = id
        };

        await _mediator.Publish(command);

        return Ok();
    }
}