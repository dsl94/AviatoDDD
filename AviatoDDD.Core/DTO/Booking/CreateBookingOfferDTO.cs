using System.ComponentModel.DataAnnotations;

namespace AviatoDDD.Domain.DTO.Booking;

public class CreateBookingOfferDTO
{
    [Required]
    public Guid FlightId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public string ClassType { get; set; }
    [Required]
    public int PointsToUse { get; set; }
}