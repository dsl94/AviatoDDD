using AviatoDDD.Domain.DTO.Airplane;

namespace AviatoDDD.Domain.Services;

public interface IAirplaneService
{
    Task<List<AirplaneDTO>> GetAllAsync();
    Task<AirplaneDTO> GetOneAsync(Guid id);
    Task<AirplaneDTO> CreateAsync(AddAirplaneRequestDTO airplane);
    Task<AirplaneDTO> UpdateAsync(Guid id, AddAirplaneRequestDTO airplane);
    Task<AirplaneDTO> DeleteAsync(Guid id);
}