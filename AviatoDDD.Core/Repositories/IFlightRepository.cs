using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface IFlightRepository
{
    Task<List<Flight>> GetAllAsync();
    Task<Flight?> GetOneAsync(Guid id);
    Task<Flight> CreateAsync(Flight region);
    Task<Flight?> UpdateAsync(Guid id, Flight region);
    Task<Flight?> DeleteAsync(Guid id);
}