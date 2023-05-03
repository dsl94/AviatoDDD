using AviatoDDD.Domain.DTO.Airplane;

namespace AviatoDDD.Domain.DTO.Flight;

public class LoadFactorDTO
{
    public AirplaneDTO Airplane { get; set; }
    public int EconomyClassSoldTickets { get; set; }
    public int BusinessClassSoldTickets { get; set; }
    public int FirstClassSoldTickets { get; set; }
    public int SoldToFemale { get; set; }
    public int SoldToMale { get; set; }
    public int SoldToChildren { get; set; }
    public int Reserved { get; set; }
    public float TotalEarnings { get; set; }
}