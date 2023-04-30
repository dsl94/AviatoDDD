using AviatoDDD.Domain.DTO.Airplane;

namespace AviatoDDD.Domain.DTO.Flight;

public class FlightDTO
{
    public Guid Id { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime DateAndTime { get; set; }
    public float BasePrice { get; set; }
    public AirplaneDTO Airplane { get; set; }
}