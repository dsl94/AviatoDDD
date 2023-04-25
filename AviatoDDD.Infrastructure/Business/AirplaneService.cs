using AutoMapper;
using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;

namespace AviatoDDD.Repository.Business;

public class AirplaneService: IAirplaneService
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;

    public AirplaneService(IAirplaneRepository airplaneRepository, IMapper mapper)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
    }
    
    public async Task<List<AirplaneDTO>> GetAllAsync()
    {
        var airplanes = await _airplaneRepository.GetAllAsync();
        return _mapper.Map<List<AirplaneDTO>>(airplanes);
    }

    public async Task<AirplaneDTO?> GetOneAsync(Guid Id)
    {
        var airplane = await _airplaneRepository.GetOneAsync(Id);
        if (airplane != null)
        {
            return _mapper.Map<AirplaneDTO>(airplane);
        }

        return null;
    }

    public async Task<AirplaneDTO> CreateAsync(AddAirplaneRequestDTO airplane)
    {
        var entity = _mapper.Map<Airplane>(airplane);
        entity = await _airplaneRepository.CreateAsync(entity);

        return _mapper.Map<AirplaneDTO>(entity);
    }

    public async Task<AirplaneDTO?> UpdateAsync(Guid Id, AddAirplaneRequestDTO airplane)
    {
        var entity = _mapper.Map<Airplane>(airplane);
        entity = await _airplaneRepository.UpdateAsync(Id, entity);

        return _mapper.Map<AirplaneDTO>(entity);
    }

    public async Task<AirplaneDTO?> DeleteAsync(Guid id)
    {
        var deleted = await _airplaneRepository.DeleteAsync(id);

        return _mapper.Map<AirplaneDTO>(deleted);
    }
}