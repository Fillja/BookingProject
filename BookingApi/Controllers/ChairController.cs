using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChairController(ChairService chairService, ChairRepository chairRepository) : ControllerBase
{
    private readonly ChairRepository _chairRepository = chairRepository;
    private readonly ChairService _chairService = chairService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(ChairModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _chairService.CreateChairAsync(model);

            if (createResult.StatusCode.Equals(0))
                return Created($"/api/chair/create/{createResult.Content}", createResult.Content);

            else if (createResult.StatusCode.Equals(2))
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _chairRepository.GetAllAsync();

        if (listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if (listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _chairRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode.Equals(0))
            return Ok(getResult.Content);

        else if (getResult.StatusCode.Equals(2))
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, ChairUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            var updateResult = await _chairService.UpdateChairAsync(id, model);

            if (updateResult.StatusCode.Equals(0))
                return Ok(updateResult.Content);

            else if (updateResult.StatusCode.Equals(2))
                return NotFound(updateResult.Message);

            return BadRequest(updateResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _chairRepository.DeleteAsync(x => x.Id == id);

        if (deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        else if (deleteResult.StatusCode.Equals(2))
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}