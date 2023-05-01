using AutoMapper;
using AviatoDDD.Domain.Configurations;
using AviatoDDD.Domain.DTO.Booking;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;
using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using AviatoDDD.Repository.Utilities;
using Microsoft.Extensions.Configuration;

namespace AviatoDDD.Repository.Business;

public class BookingService: IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly BookingProperties? _bookingProperties;

    public BookingService(
        IBookingRepository bookingRepository,
        IFlightRepository flightRepository,
        ICustomerRepository customerRepository,
        IConfiguration configuration,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _bookingProperties = configuration.GetSection("BookingProperties").Get<BookingProperties>();
    }

    public async Task<BookingDTO> CreateBookingOffer(CreateBookingOfferDTO dto)
    {
        await CheckIfAlreadyHasBooking(dto.FlightId, dto.CustomerId);
        
        var booking = _mapper.Map<Booking>(dto);
        var classType = Enum.Parse<ClassType>(dto.ClassType);

        // Fetch customer
        var customer = await GetCustomer(dto.CustomerId);
        OfferValidationUtility.ValidatePoints(customer.Points, booking.PointsToUse);
        
        // Fetch flight
        var flight = await GetFlight(dto.FlightId);
        OfferValidationUtility.ValidateIfFlightCanBeBookedAtThisMoment(flight.DateAndTime);
        OfferValidationUtility.CheckIfFlightHasFreeSeats(flight, booking.ClassType);
        
        var offeredPrice =
            OfferCalculationUtility.CalculatePrice(
                flight.BasePrice,
                customer.CustomerType,
                classType,
                dto.PointsToUse,
                flight.DateAndTime,
                _bookingProperties);

        booking.ClassType = classType;
        booking.Customer = customer;
        booking.Flight = flight;
        booking.Price = offeredPrice;
        booking.BookingStatus = BookingStatus.Offered;

        booking = await _bookingRepository.CreateAsync(booking);

        customer.Points -= booking.PointsToUse;
        await _customerRepository.UpdateAsync(customer);

        return _mapper.Map<BookingDTO>(booking);
    }

    private async Task<Customer> GetCustomer(Guid customerId)
    {
        var customer = await _customerRepository.GetOneAsync(customerId);
        if (customer != null)
        {
            return customer;
        }

        throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Customer with id: " + customerId + " not found");
    }

    private async Task<Flight> GetFlight(Guid flightId)
    {
        var flight = await _flightRepository.GetOneAsync(flightId);
        if (flight != null)
        {
            return flight;
        }

        throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Flight with id: " + flightId + " not found");
    }

    private async Task<Boolean> CheckIfAlreadyHasBooking(Guid flightId, Guid customerId)
    {
        var booking = await _bookingRepository.FindByFlightIdAndCustomerId(flightId, customerId);
        if (booking != null)
        {
            throw new BookingCreationException(ErrorCode.AlreadyHasBooking, "Customer already has booking for flight with id: " + flightId);
        }

        return true;
    }
}