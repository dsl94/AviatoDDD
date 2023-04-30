using System.ComponentModel.DataAnnotations;

namespace AviatoDDD.Domain.DTO.Flight;

public class AddFlightRequestDTO
{
    [Required]
    public string DepartureAirport { get; set; }
    [Required]
    public string ArrivalAirport { get; set; }
    [Required]
    public DateTime DateAndTime { get; set; }
    [Required]
    public float BasePrice { get; set; }
    [Required]
    public Guid AirplaneId { get; set; }
}