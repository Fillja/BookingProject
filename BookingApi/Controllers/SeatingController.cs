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

            if (createResult.StatusCode.Equals(0))
                return Created($"/api/seating/create/{createResult.Content}", createResult.Content);

            else if (createResult.StatusCode.Equals(2))
                return NotFound(createResult.Message!);

            return BadRequest(createResult.Message!);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _seatingService.GetAllSeatingsAsync();

        if(listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if(listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message!);

        return BadRequest(listResult.Message!);
    }

    [HttpGet("getone/{tableId}")]
    public async Task<IActionResult> GetOne(string tableId)
    {
        var getResult = await _seatingService.GetOneSeatingAsync(tableId);

        if(getResult.StatusCode.Equals(0))
            return Ok(getResult.Content);

        else if(getResult.StatusCode.Equals(2))
            return NotFound(getResult.Message!);

        return BadRequest(getResult.Message!); 
    }

    [HttpDelete("delete/{tableId}")]
    public async Task<IActionResult> Delete(string tableId)
    {
        var seatingListResult = await _seatingRepository.GetAllWithTableIdAsync(tableId);

        if(seatingListResult.StatusCode.Equals(0))
        {
            var seatingList = (IEnumerable<SeatingEntity>)seatingListResult.Content!;

            var deleteResult = await _seatingRepository.DeleteMultipleAsync(seatingList);

            if(deleteResult.StatusCode.Equals(0))
                return Ok(deleteResult.Message);

            return BadRequest(deleteResult.Message!);

        }
        else if(seatingListResult.StatusCode.Equals(2))
            return NotFound(seatingListResult.Message!);

        return BadRequest(seatingListResult.Message!);
    }

    [HttpPost("addchair/{chairId}")]
    public async Task<IActionResult> AddChair(SeatingModel model, string chairId)
    {
        var createResult = await _seatingService.CreateSeatingEntityAsync(model, chairId);

        if(createResult.StatusCode.Equals(0))
            return Ok(createResult.Message);

        else if(createResult.StatusCode.Equals(2))
            return NotFound(createResult.Message);

        return BadRequest(createResult.Message);
    }

    [HttpDelete("removechair/{chairId}")]
    public async Task<IActionResult> RemoveChair(SeatingModel model, string chairId)
    {
        var deleteResult = await _seatingRepository.DeleteAsync(s => s.ChairId == chairId);

        if (deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
