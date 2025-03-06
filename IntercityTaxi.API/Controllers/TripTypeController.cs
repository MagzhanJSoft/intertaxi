using IntercityTaxi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntercityTaxi.API.Controllers;
[ApiController]
[Route("[controller]")]
public class TripTypeController : ControllerBase
{
    private readonly TripTypeService _tripTypeService;

    public TripTypeController(TripTypeService tripTypeService)
    {
        _tripTypeService = tripTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var result = await _tripTypeService.GetAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTripByGuid(Guid id)
    {
        var result = await _tripTypeService.GetAsyncByGuid(id);
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
            return BadRequest("Name of trip must not be empty.");

        var result = await _tripTypeService.CreateAsync(name);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _tripTypeService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);

    }
}
