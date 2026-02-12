using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Global.Host.Controllers;

[ApiController]
[Route("api/v1/countries")]

public class CountriesController : ControllerBase
{
    private readonly ICountryManager _countryManager;

    public CountriesController(ICountryManager countryManager)
    {
        _countryManager = countryManager;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Country>>> Get()
    {
        var countries = await _countryManager.GetAll();
        return Ok(countries);
    }
}
