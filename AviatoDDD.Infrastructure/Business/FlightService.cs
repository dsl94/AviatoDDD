using AutoMapper;
using AviatoDDD.Domain.DTO.Flight;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;
using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using Microsoft.Extensions.Logging;

namespace AviatoDDD.Repository.Business;

public class FlightService: IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FlightService> _logger;

    public FlightService(IFlightRepository flightRepository, IAirplaneRepository airplaneRepository, IMapper mapper, ILogger<FlightService> logger)
    {
        _flightRepository = flightRepository;
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<FlightDTO>> GetAllAsync()
    {
        var flights = await _flightRepository.GetAllAsync();
        return _mapper.Map<List<FlightDTO>>(flights);
    }

    public async Task<FlightDTO> GetOneAsync(Guid id)
    {
        var flight = await _flightRepository.GetOneAsync(id);
        if (flight != null)
        {
            return _mapper.Map<FlightDTO>(flight);
        }

        throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Flight with id: " + id + " not found");
    }

    public async Task<FlightDTO> CreateAsync(AddFlightRequestDTO flight)
    {
        var entity = _mapper.Map<Flight>(flight);
        var airplane = await _airplaneRepository.GetOneAsync(flight.AirplaneId);
        if (airplane == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Airplane with id: " + flight.AirplaneId + " not found");
        }

        entity.Airplane = airplane;
        entity = await _flightRepository.CreateAsync(entity);

        return _mapper.Map<FlightDTO>(entity);
    }

    public async Task<FlightDTO> UpdateAsync(Guid id, AddFlightRequestDTO flight)
    {
        var existing = await _flightRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Flight with id: " + id + " not found");
        }
        
        existing.DepartureAirport = flight.DepartureAirport;
        existing.ArrivalAirport = flight.ArrivalAirport;
        existing.DateAndTime = flight.DateAndTime;
        existing.BasePrice = flight.BasePrice;

        if (!flight.AirplaneId.Equals(existing.AirplaneId))
        {
            var airplane = await _airplaneRepository.GetOneAsync(flight.AirplaneId);
            if (airplane == null)
            {
                throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Airplane with id: " + flight.AirplaneId + " not found");
            }
            existing.Airplane = airplane;
        }

        existing = await _flightRepository.UpdateAsync(existing);

        return _mapper.Map<FlightDTO>(existing);
    }

    public async Task<FlightDTO> DeleteAsync(Guid id)
    {
        var existing = await _flightRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Flight with id: " + id + " not found");
        }
        var deleted = await _flightRepository.DeleteAsync(existing);

        return _mapper.Map<FlightDTO>(deleted);
    }
}