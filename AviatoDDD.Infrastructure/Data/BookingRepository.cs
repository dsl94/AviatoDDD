using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AviatoDDD.Domain.Data;

public class BookingRepository: IBookingRepository
{
    private readonly AviatoDbContext _dbContext;
    
    public BookingRepository(AviatoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Booking>> GetAllAsync()
    {
        return await _dbContext.Bookings
            .Include(booking => booking.Flight)
            .Include(booking => booking.Customer)
            .ToListAsync();
    }

    public async Task<Booking?> GetOneAsync(Guid id)
    {
        return await _dbContext.Bookings
            .Include(booking => booking.Flight)
            .Include(booking => booking.Customer)
            .SingleOrDefaultAsync(booking => booking.Id == id);
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        await _dbContext.Bookings.AddAsync(booking);
        await _dbContext.SaveChangesAsync();

        return booking;
    }

    public async Task<Booking?> UpdateAsync(Booking booking)
    {
        await _dbContext.SaveChangesAsync();
        
        return booking;
    }

    public async Task<Booking?> DeleteAsync(Booking booking)
    {
        _dbContext.Bookings.Remove(booking);
        await _dbContext.SaveChangesAsync();
        return booking;
    }
}