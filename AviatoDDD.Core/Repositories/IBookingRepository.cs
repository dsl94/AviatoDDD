using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<List<Booking>> GetAllForCustomerAsync(Guid customerId);
    Task<List<Booking>> GetAllForFlightAsync(Guid flightId);
    Task<Booking?> GetOneAsync(Guid id);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking?> UpdateAsync(Booking booking);
    Task<Booking?> DeleteAsync(Booking booking);

    Task<Booking?> FindByFlightIdAndCustomerId(Guid flightId, Guid customerId);
}