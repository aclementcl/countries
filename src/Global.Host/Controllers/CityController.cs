using Global.Manager.Dtos;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Global.Host.Controllers;

[ApiController]
[Route("api/v1/cities")]
[Authorize]

public class CitiesController : ControllerBase
{
    private readonly ICityManager _cityManager;

    public CitiesController(ICityManager cityManager)
    {
        _cityManager = cityManager;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CityDto>>> GetAll()
    {
        var cities = await _cityManager.GetAll();
        var result = cities
            .Select(city => new CityDto { Id = city.Id, Name = city.Name, CountryId = city.CountryId })
            .ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CityDto>> GetById(int id)
    {
        var city = await _cityManager.GetById(id);
        if (city is null)
        {
            return NotFound();
        }

        return Ok(new CityDto { Id = city.Id, Name = city.Name, CountryId = city.CountryId });
    }

    [HttpPost]
    public async Task<ActionResult<CityDto>> Create([FromBody] CreateCityDto city)
    {
        var created = await _cityManager.Create(new City { Name = city.Name, CountryId = city.CountryId });
        var result = new CityDto { Id = created.Id, Name = created.Name, CountryId = created.CountryId };

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCityDto city)
    {
        var updated = await _cityManager.Update(id, new City { Name = city.Name, CountryId = city.CountryId });
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _cityManager.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
