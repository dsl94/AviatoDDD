using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetOneAsync(Guid id);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking?> UpdateAsync(Booking booking);
    Task<Booking?> DeleteAsync(Booking booking);
}