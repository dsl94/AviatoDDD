using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetOneAsync(Guid id);
    Task<Customer> CreateAsync(Customer region);
    Task<Customer?> UpdateAsync(Guid id, Customer region);
    Task<Customer?> DeleteAsync(Guid id);
}