using AutoMapper;
using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;
using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using Microsoft.Extensions.Logging;

namespace AviatoDDD.Repository.Business;

public class CustomerService: ICustomerService
{
    private readonly ICrudRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICrudRepository<Customer> customerRepository, IMapper mapper, ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<CustomerDTO>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<List<CustomerDTO>>(customers);
    }

    public async Task<CustomerDTO> GetOneAsync(Guid id)
    {
        var customer = await _customerRepository.GetOneAsync(id);
        if (customer != null)
        {
            return _mapper.Map<CustomerDTO>(customer);
        }

        throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Customer with id: " + id + " not found");
    }

    public async Task<CustomerDTO> CreateAsync(AddCustomerRequestDTO customer)
    {
        var entity = _mapper.Map<Customer>(customer);
        entity = await _customerRepository.CreateAsync(entity);

        return _mapper.Map<CustomerDTO>(entity);
    }

    public async Task<CustomerDTO> UpdateAsync(Guid id, AddCustomerRequestDTO customer)
    {
        var existing = await _customerRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Customer with id: " + id + " not found");
        }
        
        existing.Name = customer.Name;
        existing.Points = customer.Points;
        existing.CustomerType = Enum.Parse<CustomerType>(customer.CustomerType);

        existing = await _customerRepository.UpdateAsync(existing);

        return _mapper.Map<CustomerDTO>(existing);
    }

    public async Task<CustomerDTO> DeleteAsync(Guid id)
    {
        var existing = await _customerRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Customer with id: " + id + " not found");
        }
        var deleted = await _customerRepository.DeleteAsync(existing);

        return _mapper.Map<CustomerDTO>(deleted);
    }
}