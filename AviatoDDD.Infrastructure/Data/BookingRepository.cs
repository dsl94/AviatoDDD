using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;

namespace AviatoDDD.Domain.Data;

public class BookingRepository: IBookingRepository
{
    private readonly AviatoDbContext _dbContext;
    
    public BookingRepository(AviatoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<Booking>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Booking?> GetOneAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Booking> CreateAsync(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Task<Booking?> UpdateAsync(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Task<Booking?> DeleteAsync(Booking booking)
    {
        throw new NotImplementedException();
    }
}