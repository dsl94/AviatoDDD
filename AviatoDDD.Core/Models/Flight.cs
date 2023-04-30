using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Models;

public class Flight
{
    public Guid Id { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime DateAndTime { get; set; }
    public float BasePrice { get; set; }
    public Guid AirplaneId { get; set; }

    // Navigation properties
    public Airplane Airplane { get; set; }
    public List<Booking> Bookings { get; set; }
}