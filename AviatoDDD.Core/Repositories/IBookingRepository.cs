using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetOneAsync(Guid id);
    Task<Booking> CreateAsync(Booking region);
    Task<Booking?> UpdateAsync(Guid id, Booking region);
    Task<Booking?> DeleteAsync(Guid id);
}