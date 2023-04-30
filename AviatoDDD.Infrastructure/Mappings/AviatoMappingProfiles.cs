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
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerType, 
                act => act.MapFrom
                    (src => src.CustomerType.ToString()));
        CreateMap<AddCustomerRequestDTO, Customer>()
            .ForMember(dest => dest.CustomerType, 
                act => act.MapFrom
                    (src => Enum.Parse<CustomerType>(src.CustomerType)));
    }
}