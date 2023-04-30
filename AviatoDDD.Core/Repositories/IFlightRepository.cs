using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface IFlightRepository
{
    Task<List<Flight>> GetAllAsync();
    Task<Flight?> GetOneAsync(Guid id);
    Task<Flight> CreateAsync(Flight flight);
    Task<Flight> UpdateAsync(Flight flight);
    Task<Flight> DeleteAsync(Flight flight);
}