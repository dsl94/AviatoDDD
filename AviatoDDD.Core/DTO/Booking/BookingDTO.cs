using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.DTO.Flight;

namespace AviatoDDD.Domain.DTO.Booking;

public class BookingDTO
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public string ClassType { get; set; }
    public string BookingStatus { get; set; }
    public int PointsToUse { get; set; }
    public DateTime CreatedAt { get; set; }
    public FlightDTO Flight { get; set; }
    public CustomerDTO Customer { get; set; }
}