using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Repositories;

public interface IAirplaneRepository
{
    Task<List<Airplane>> GetAllAsync();
    Task<Airplane?> GetOneAsync(Guid id);
    Task<Airplane> CreateAsync(Airplane region);
    Task<Airplane?> UpdateAsync(Guid id, Airplane region);
    Task<Airplane?> DeleteAsync(Guid id);
}