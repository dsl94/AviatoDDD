using AutoMapper;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using Microsoft.Extensions.Logging;

namespace AviatoDDD.Repository.Business;

public class AirplaneService: IAirplaneService
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AirplaneService> _logger;

    public AirplaneService(IAirplaneRepository airplaneRepository, IMapper mapper, ILogger<AirplaneService> logger)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<AirplaneDTO>> GetAllAsync()
    {
        _logger.LogInformation("Reading all airplanes");
        var airplanes = await _airplaneRepository.GetAllAsync();
        _logger.LogInformation("Returning all airplanes");
        return _mapper.Map<List<AirplaneDTO>>(airplanes);
    }

    public async Task<AirplaneDTO> GetOneAsync(Guid id)
    {
        var airplane = await _airplaneRepository.GetOneAsync(id);
        if (airplane != null)
        {
            return _mapper.Map<AirplaneDTO>(airplane);
        }

        throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Airplane with id: " + id + " not found");
    }

    public async Task<AirplaneDTO> CreateAsync(AddAirplaneRequestDTO airplane)
    {
        var entity = _mapper.Map<Airplane>(airplane);
        entity = await _airplaneRepository.CreateAsync(entity);

        return _mapper.Map<AirplaneDTO>(entity);
    }

    public async Task<AirplaneDTO> UpdateAsync(Guid id, AddAirplaneRequestDTO airplane)
    {
        var existing = await _airplaneRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Airplane with id: " + id + " not found");
        }
        
        existing.Name = airplane.Name;
        existing.EconomyClassCapacity = airplane.EconomyClassCapacity;
        existing.BusinessClassCapacity = airplane.BusinessClassCapacity;
        existing.FirstClassCapacity = airplane.FirstClassCapacity;
        
        existing = await _airplaneRepository.UpdateAsync(existing);

        return _mapper.Map<AirplaneDTO>(existing);
    }

    public async Task<AirplaneDTO> DeleteAsync(Guid id)
    {
        var existing = await _airplaneRepository.GetOneAsync(id);
        if (existing == null)
        {
            throw new EntityNotFoundException(ErrorCode.EntityNotFound, "Airplane with id: " + id + " not found");
        }
        var deleted = await _airplaneRepository.DeleteAsync(existing);

        return _mapper.Map<AirplaneDTO>(deleted);
    }
}