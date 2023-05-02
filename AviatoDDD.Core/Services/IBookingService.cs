using AviatoDDD.Domain.DTO.Booking;

namespace AviatoDDD.Domain.Services;

public interface IBookingService
{
    Task<List<BookingDTO>> GetAllAsync();
    Task<List<BookingDTO>> GetAllForCustomerAsync(Guid customerId);
    Task<List<BookingDTO>> GetAllForFlightAsync(Guid flightId);
    Task<BookingDTO> GetOneAsync(Guid id);
    Task<BookingDTO> CreateBookingOfferAsync(CreateBookingOfferDTO dto);
    Task<BookingDTO> AcceptBookingOfferAsync(Guid id);
    Task<BookingDTO> DeclineBookingOfferAsync(Guid id);
}