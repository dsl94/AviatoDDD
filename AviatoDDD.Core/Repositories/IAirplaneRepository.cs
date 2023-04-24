using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Repositories;

public interface IAirplaneRepository
{
    Task<List<Airplane>> GetAllAsync();
    Task<Airplane?> GetOneAsync(Guid id);
    Task<Airplane> CreateAsync(Airplane airplane);
    Task<Airplane?> UpdateAsync(Guid id, Airplane airplane);
    Task<Airplane?> DeleteAsync(Guid id);
}