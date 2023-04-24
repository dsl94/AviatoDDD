using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AviatoDDD.Domain.Data;

public class AirplaneRepository: IAirplaneRepository
{
    private readonly AviatoDbContext _dbContext;
    
    public AirplaneRepository(AviatoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Airplane>> GetAllAsync()
    {
        return await _dbContext.Airplanes.ToListAsync();
    }

    public async Task<Airplane?> GetOneAsync(Guid id)
    {
        return await _dbContext.Airplanes.FindAsync(id);
    }

    public async Task<Airplane> CreateAsync(Airplane airplane)
    {
        await _dbContext.Airplanes.AddAsync(airplane);
        await _dbContext.SaveChangesAsync();

        return airplane;
    }

    public async Task<Airplane?> UpdateAsync(Guid id, Airplane airplane)
    {
        var existing = await _dbContext.Airplanes.FindAsync(id);
        if (existing == null)
        {
            return null;
        }
        existing.Name = airplane.Name;
        existing.EconomyClassCapacity = airplane.EconomyClassCapacity;
        existing.BusinessClassCapacity = airplane.BusinessClassCapacity;
        existing.FirstClassCapacity = airplane.FirstClassCapacity;

        await _dbContext.SaveChangesAsync();

        return existing;
    }

    public async Task<Airplane?> DeleteAsync(Guid id)
    {
        var existing = await _dbContext.Airplanes.FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        _dbContext.Airplanes.Remove(existing);
        await _dbContext.SaveChangesAsync();
        return existing;
    }
}