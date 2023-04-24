namespace AviatoDDD.Domain.DTO.Airplane;

public class AddAirplaneRequestDTO
{
    public string Name { get; set; }
    public int EconomyClassCapacity { get; set; }
    public int BusinessClassCapacity { get; set; }
    public int FirstClassCapacity { get; set; }
}