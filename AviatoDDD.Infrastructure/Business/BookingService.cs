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
    private readonly ICrudRepository<Flight> _flightRepository;
    private readonly ICrudRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly BookingProperties? _bookingProperties;

    public BookingService(
        IBookingRepository bookingRepository,
        ICrudRepository<Flight> flightRepository,
        ICrudRepository<Customer> customerRepository,
        IConfiguration configuration,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _bookingProperties = configuration.GetSection("BookingProperties").Get<BookingProperties>();
    }

    public async Task<List<BookingDTO>> GetAllAsync()
    {
        var all = await _bookingRepository.GetAllAsync();
        
        return _mapper.Map<List<BookingDTO>>(all);
    }

    public async Task<List<BookingDTO>> GetAllForCustomerAsync(Guid customerId)
    {
        var all = await _bookingRepository.GetAllForCustomerAsync(customerId);
        
        return _mapper.Map<List<BookingDTO>>(all);
    }

    public async Task<List<BookingDTO>> GetAllForFlightAsync(Guid flightId)
    {
        var all = await _bookingRepository.GetAllForFlightAsync(flightId);
        
        return _mapper.Map<List<BookingDTO>>(all);
    }

    public async Task<BookingDTO> GetOneAsync(Guid id)
    {
        var booking = await _bookingRepository.GetOneAsync(id);

        return _mapper.Map<BookingDTO>(booking);
    }

    public async Task<BookingDTO> CreateBookingOfferAsync(CreateBookingOfferDTO dto)
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
        booking.CreatedAt = DateTime.Now;

        booking = await _bookingRepository.CreateAsync(booking);

        return _mapper.Map<BookingDTO>(booking);
    }

    public async Task<BookingDTO> AcceptBookingOfferAsync(Guid id)
    {
        var booking = await _bookingRepository.GetOneAsync(id);
        if (booking == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Booking with id: " + id + " not found");
        }
        
        // Validate if offer can be accepted
        OfferValidationUtility.ValidateIfOfferCanBeAccepted(booking);
        if (OfferValidationUtility.IsOfferExpired(booking.CreatedAt))
        {
            await _bookingRepository.DeleteAsync(booking);
            throw new BookingCreationException(ErrorCode.OfferExpired, "Booking offer expired and it is deleted, please create new one");
        }

        booking.BookingStatus = BookingStatus.Confirmed;

        booking = await _bookingRepository.UpdateAsync(booking);

        var customer = booking.Customer;
        customer.Points -= booking.PointsToUse;
        // add points
        var pointsToAdd = booking.Flight.BasePrice * _bookingProperties!.PointsPercentage / 100;
        customer.Points += (int)pointsToAdd;
        await _customerRepository.UpdateAsync(customer);

        return _mapper.Map<BookingDTO>(booking);
    }

    public async Task<BookingDTO> DeclineBookingOfferAsync(Guid id)
    {
        var booking = await _bookingRepository.GetOneAsync(id);
        if (booking == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Booking with id: " + id + " not found");
        }
        
        OfferValidationUtility.ValidateIfOfferCanBeDeclined(booking);

        booking = await _bookingRepository.DeleteAsync(booking);
        
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