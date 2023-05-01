using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AviatoDDD.Domain.Data;

public class FlightRepository: IFlightRepository
{
    private readonly AviatoDbContext _dbContext;
    
    public FlightRepository(AviatoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Flight>> GetAllAsync()
    {
        return await _dbContext.Flights
            .Include(flight => flight.Airplane).ToListAsync();
    }

    public async Task<Flight?> GetOneAsync(Guid id)
    {
        return await _dbContext.Flights
            .Include(flight => flight.Airplane)
            .Include(flight => flight.Bookings)
            .SingleOrDefaultAsync(flight => flight.Id == id);
    }

    public async Task<Flight> CreateAsync(Flight flight)
    {
        await _dbContext.Flights.AddAsync(flight);
        await _dbContext.SaveChangesAsync();

        return flight;
    }

    public async Task<Flight> UpdateAsync(Flight flight)
    {
        await _dbContext.SaveChangesAsync();

        return flight;
    }

    public async Task<Flight> DeleteAsync(Flight flight)
    {
        _dbContext.Flights.Remove(flight);
        await _dbContext.SaveChangesAsync();
        return flight;
    }
}