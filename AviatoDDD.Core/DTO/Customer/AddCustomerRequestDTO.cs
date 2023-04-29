using System.ComponentModel.DataAnnotations;
using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.DTO.Customer;

public class AddCustomerRequestDTO
{
    [Required]
    public string Name { get; set; }
    public int Points { get; set; } = 0;
    [Required]
    public CustomerType CustomerType { get; set; }
}