using AviatoDDD.Domain.Models;

namespace AviatoDDD.Domain.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetOneAsync(Guid id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer?> UpdateAsync(Customer customer);
    Task<Customer?> DeleteAsync(Customer customer);
}