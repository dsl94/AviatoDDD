using AviatoDDD.Domain.DTO.Booking;

namespace AviatoDDD.Queries;

public class GetBookingByIdQuery: IQuery<BookingDTO>
{
    public Guid Id { get; set; }
}