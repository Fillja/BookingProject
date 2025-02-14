using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatingController(SeatingRepository seatingRepository, SeatingService seatingService) : ControllerBase
{
    private readonly SeatingRepository _seatingRepository = seatingRepository;
    private readonly SeatingService _seatingService = seatingService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(SeatingModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _seatingService.CreateSeatingAsync(model);

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"/api/seating/create/{createResult.Content}", createResult);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message!);

            return BadRequest(createResult.Message!);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _seatingRepository.GetAllAsync();

        if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult);

        else if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message!);

        return BadRequest(listResult.Message!);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _seatingService.GetOneSeatingAsync(id);

        if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getResult);

        else if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(getResult.Message!);

        return BadRequest(getResult.Message!); 
    }
}
