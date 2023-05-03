using AviatoDDD.Domain.DTO.Flight;

namespace AviatoDDD.Domain.Services;

public interface IFlightService
{
    Task<List<FlightDTO>> GetAllAsync();
    Task<FlightDTO> GetOneAsync(Guid id);
    Task<FlightDTO> CreateAsync(AddFlightRequestDTO flight);
    Task<FlightDTO> UpdateAsync(Guid id, AddFlightRequestDTO flight);
    Task<FlightDTO> DeleteAsync(Guid id);
    Task<LoadFactorDTO> GetLoadAsync(Guid id);
}