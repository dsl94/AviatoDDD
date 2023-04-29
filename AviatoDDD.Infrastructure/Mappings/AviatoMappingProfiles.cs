using AutoMapper;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Models;

namespace AviatoDDD.Repository.Mappings;

public class AviatoMappingProfiles: Profile
{
    public AviatoMappingProfiles()
    {
        // Airplane Mappings
        CreateMap<Airplane, AirplaneDTO>().ReverseMap();
        CreateMap<Airplane, AddAirplaneRequestDTO>().ReverseMap();
        
        // Customer Mappings
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Customer, AddCustomerRequestDTO>().ReverseMap();
    }
}