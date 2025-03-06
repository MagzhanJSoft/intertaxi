using Microsoft.AspNetCore.Mvc;
using IntercityTaxi.Application.Service;

namespace IntercityTaxi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{
    private readonly CityService _cityService;

    public CitiesController(CityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var result = await _cityService.GetAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCityByGuid(Guid id)
    {
        var result = await _cityService.GetAsyncByGuid(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name of city must not be empty.");

        var result = await _cityService.CreateAsync(name);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _cityService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

}
