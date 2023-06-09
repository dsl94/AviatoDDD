namespace AviatoDDD.Domain.Models;

public class Booking
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public ClassType ClassType { get; set; }
    public BookingStatus BookingStatus { get; set; }
    public Guid FlightId { get; set; }
    public Guid CustomerId { get; set; }
    public int PointsToUse { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Flight Flight { get; set; }
    public Customer Customer { get; set; }
}