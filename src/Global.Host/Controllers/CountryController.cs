using Global.Manager.Dtos;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Global.Host.Controllers;

[ApiController]
[Route("api/v1/countries")]
[Authorize]

public class CountriesController : ControllerBase
{
    private readonly ICountryManager _countryManager;

    public CountriesController(ICountryManager countryManager)
    {
        _countryManager = countryManager;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CountryDto>>> GetAll()
    {
        var countries = await _countryManager.GetAll();
        var result = countries
            .Select(country => new CountryDto { Id = country.Id, Name = country.Name })
            .ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryDto>> GetById(int id)
    {
        var country = await _countryManager.GetById(id);
        if (country is null)
        {
            return NotFound();
        }

        return Ok(new CountryDto { Id = country.Id, Name = country.Name });
    }

    [HttpPost]
    public async Task<ActionResult<CountryDto>> Create([FromBody] CreateCountryDto country)
    {
        var created = await _countryManager.Create(new Country { Name = country.Name });
        var result = new CountryDto { Id = created.Id, Name = created.Name };

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto country)
    {
        var updated = await _countryManager.Update(id, new Country { Name = country.Name });
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _countryManager.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
