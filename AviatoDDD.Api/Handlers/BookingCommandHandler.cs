using AviatoDDD.Commands;
using AviatoDDD.Domain.Services;
using AviatoDDD.Repository.Business;

namespace AviatoDDD.Handlers;

public class BookingCommandHandler: ICommandHandler<CreateBookingOfferCommand>,
    ICommandHandler<AcceptBookingCommand>,
    ICommandHandler<DeclineBookingCommand>
{
    private readonly IBookingService _bookingService;

    public BookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    public async Task Handle(CreateBookingOfferCommand notification, CancellationToken cancellationToken)
    {
        await _bookingService.CreateBookingOfferAsync(notification.BookingOffer);
    }

    public async Task Handle(AcceptBookingCommand notification, CancellationToken cancellationToken)
    {
        await _bookingService.AcceptBookingOfferAsync(notification.Id);
    }

    public async Task Handle(DeclineBookingCommand notification, CancellationToken cancellationToken)
    {
        await _bookingService.DeclineBookingOfferAsync(notification.Id);
    }
}