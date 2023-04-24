namespace AviatoDDD.Domain.DTO.Airplane;

public class AirplaneDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int EconomyClassCapacity { get; set; }
    public int BusinessClassCapacity { get; set; }
    public int FirstClassCapacity { get; set; }
}