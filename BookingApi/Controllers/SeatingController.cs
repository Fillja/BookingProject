using Infrastructure.Entities;
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
    public async Task<IActionResult> Create(SeatingCreateModel model)
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
        var listResult = await _seatingService.GetAllSeatingsAsync();

        if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult);

        else if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message!);

        return BadRequest(listResult.Message!);
    }

    [HttpGet("getone/{tableId}")]
    public async Task<IActionResult> GetOne(string tableId)
    {
        var getResult = await _seatingService.GetOneSeatingAsync(tableId);

        if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getResult);

        else if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(getResult.Message!);

        return BadRequest(getResult.Message!); 
    }

    [HttpDelete("delete/{tableId}")]
    public async Task<IActionResult> Delete(string tableId)
    {
        var seatingListResult = await _seatingRepository.GetAllWithTableIdAsync(tableId);

        if(seatingListResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
        {
            var seatingList = (IEnumerable<SeatingEntity>)seatingListResult.Content!;

            var deleteResult = await _seatingRepository.DeleteMultipleAsync(seatingList);

            if(deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
                return Ok(deleteResult.Message);

            return BadRequest(deleteResult.Message!);

        }
        else if(seatingListResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(seatingListResult.Message!);

        return BadRequest(seatingListResult.Message!);
    }

    [HttpPost("addchair/{chairId}")]
    public async Task<IActionResult> AddChair(SeatingModel model, string chairId)
    {
        var createResult = await _seatingService.CreateSeatingEntityAsync(model, chairId);

        if(createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
            return Ok(createResult.Message);

        else if(createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(createResult.Message);

        return BadRequest(createResult.Message);
    }

    [HttpDelete("removechair/{chairId}")]
    public async Task<IActionResult> RemoveChair(SeatingModel model, string chairId)
    {
        var deleteResult = await _seatingRepository.DeleteAsync(s => s.ChairId == chairId);

        if (deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
