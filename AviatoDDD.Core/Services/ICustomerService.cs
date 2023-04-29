using AviatoDDD.Domain.DTO.Customer;

namespace AviatoDDD.Domain.Services;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetAllAsync();
    Task<CustomerDTO> GetOneAsync(Guid id);
    Task<CustomerDTO> CreateAsync(AddCustomerRequestDTO customer);
    Task<CustomerDTO> UpdateAsync(Guid id, AddCustomerRequestDTO customer);
    Task<CustomerDTO> DeleteAsync(Guid id);
}