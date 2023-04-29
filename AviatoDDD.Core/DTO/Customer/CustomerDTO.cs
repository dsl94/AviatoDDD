using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.DTO.Customer;

public class CustomerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; } = 0;
    public CustomerType CustomerType { get; set; }
}