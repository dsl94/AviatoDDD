using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Repositories;

public interface IAirplaneRepository
{
    Task<List<Airplane>> GetAllAsync();
    Task<Airplane?> GetOneAsync(Guid id);
    Task<Airplane> CreateAsync(Airplane airplane);
    Task<Airplane> UpdateAsync(Airplane airplane);
    Task<Airplane> DeleteAsync(Airplane airplane);
}