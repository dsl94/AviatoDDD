using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AviatoDDD.Domain.Data;

public class CustomerRepository: ICustomerRepository
{
    private readonly AviatoDbContext _dbContext;
    
    public CustomerRepository(AviatoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Customer>> GetAllAsync()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetOneAsync(Guid id)
    {
        return await _dbContext.Customers.FindAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer?> UpdateAsync(Customer customer)
    {
        await _dbContext.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer?> DeleteAsync(Customer customer)
    {
        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }
}