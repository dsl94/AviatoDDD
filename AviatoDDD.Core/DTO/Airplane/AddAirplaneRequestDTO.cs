using System.ComponentModel.DataAnnotations;

namespace AviatoDDD.Domain.DTO.Airplane;

public class AddAirplaneRequestDTO
{
    [Required]
    [MinLength(4)]
    public string Name { get; set; }
    [Required]
    [Range(0,300)]
    public int EconomyClassCapacity { get; set; }
    [Required]
    [Range(0,20)]
    public int BusinessClassCapacity { get; set; }
    [Required]
    [Range(0,10)]
    public int FirstClassCapacity { get; set; }
}