using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; } = 0;
    public CustomerType CustomerType { get; set; }
    
    public List<Booking> Bookings { get; set; }
}