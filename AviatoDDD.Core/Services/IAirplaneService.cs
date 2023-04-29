using AviatoDDD.Domain.DTO.Airplane;

namespace AviatoDDD.Domain.Services;

public interface IAirplaneService
{
    Task<List<AirplaneDTO>> GetAllAsync();
    Task<AirplaneDTO> GetOneAsync(Guid Id);
    Task<AirplaneDTO> CreateAsync(AddAirplaneRequestDTO airplane);
    Task<AirplaneDTO> UpdateAsync(Guid id, AddAirplaneRequestDTO airplane);
    Task<AirplaneDTO> DeleteAsync(Guid id);
}