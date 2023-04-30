using AutoMapper;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.DTO.Booking;
using AviatoDDD.Domain.DTO.Customer;
using AviatoDDD.Domain.DTO.Flight;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Models;

namespace AviatoDDD.Repository.Mappings;

public class AviatoMappingProfiles: Profile
{
    public AviatoMappingProfiles()
    {
        // Airplane Mappings
        CreateMap<Airplane, AirplaneDTO>();
        CreateMap<AddAirplaneRequestDTO, Airplane>();
        
        // Customer Mappings
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerType, 
                act => act.MapFrom
                    (src => src.CustomerType.ToString()));
        CreateMap<AddCustomerRequestDTO, Customer>()
            .ForMember(dest => dest.CustomerType, 
                act => act.MapFrom
                    (src => Enum.Parse<CustomerType>(src.CustomerType)));
        
        // Flight Mappings
        CreateMap<AddFlightRequestDTO, Flight>();
        CreateMap<Flight, FlightDTO>();
        
        // Booking Mappings
        CreateMap<Booking, BookingDTO>()
            .ForMember(dest => dest.ClassType,
                act => act.MapFrom
                    (src => src.ClassType.ToString()))
            .ForMember(dest => dest.BookingStatus,
                act => act.MapFrom
                    (src => src.BookingStatus.ToString()));
        CreateMap<CreateBookingOfferDTO, Booking>()
            .ForMember(dest => dest.ClassType, 
                act => act.MapFrom
                    (src => Enum.Parse<ClassType>(src.ClassType)));
    }
}