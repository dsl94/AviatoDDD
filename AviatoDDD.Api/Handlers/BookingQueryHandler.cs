using AviatoDDD.Domain.DTO.Booking;
using AviatoDDD.Domain.Services;
using AviatoDDD.Queries;

namespace AviatoDDD.Handlers;

public class BookingQueryHandler: IQueryHandler<GetAllBookingsQuery, List<BookingDTO>>, IQueryHandler<GetBookingByIdQuery, BookingDTO>
{
    private readonly IBookingService _bookingService;

    public BookingQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    public async Task<List<BookingDTO>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetAllAsync();
    }

    public async Task<BookingDTO> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetOneAsync(request.Id);
    }
}