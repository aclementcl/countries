using Microsoft.AspNetCore.Mvc;

namespace Country.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CountriesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Chile" },
            new { Id = 2, Name = "Argentina" }
        });
    }
}
