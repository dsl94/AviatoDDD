namespace AviatoDDD.Domain.DTO.Booking;

public class CreateBookingOfferDTO
{
    public Guid FlightId { get; set; }
    public Guid CustomerId { get; set; }
    public string ClassType { get; set; }
    public int PointsToUse { get; set; }
}