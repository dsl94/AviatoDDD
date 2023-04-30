using AviatoDDD.Domain.DTO.Booking;

namespace AviatoDDD.Domain.Services;

public interface IBookingService
{
    Task<BookingDTO> CreateBookingOffer(CreateBookingOfferDTO dto);
}