using AutoMapper;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Repository.Mappings;

public class AviatoMappingProfiles: Profile
{
    public AviatoMappingProfiles()
    {
        // Airplane Mappings
        CreateMap<Airplane, AirplaneDTO>().ReverseMap();
        CreateMap<Airplane, AddAirplaneRequestDTO>().ReverseMap();
    }
}