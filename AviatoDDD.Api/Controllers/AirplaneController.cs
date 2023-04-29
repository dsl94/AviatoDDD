using AviatoDDD.Domain.DTO.Airplane;
using AviatoDDD.Domain.Services;
using AviatoDDD.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AviatoDDD.Controllers;

[ApiController]
[Route("[controller]")]
public class AirplaneController: ControllerBase
{
    private readonly IAirplaneService _airplaneService;

    public AirplaneController(IAirplaneService airplaneService)
    {
        _airplaneService = airplaneService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var airplanes = await _airplaneService.GetAllAsync();

        return Ok(airplanes);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var airplane = await _airplaneService.GetOneAsync(id);
        if (airplane == null)
        {
            return NotFound();
        }

        return Ok(airplane);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddAirplaneRequestDTO dto)
    {
        var created = await _airplaneService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] AddAirplaneRequestDTO dto, [FromRoute] Guid id)
    {
        var updated = await _airplaneService.UpdateAsync(id, dto);

        if (updated == null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _airplaneService.DeleteAsync(id);
        if (deleted == null)
        {
            return NotFound();
        }

        return Ok();
    }
    
}