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
            
            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"api/chair/create/{createResult.Content}", createResult);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _chairRepository.GetAllAsync();
        if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult);

        else if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _chairRepository.GetOneAsync(x => x.Id == id);
        if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getResult);
        
        else if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(ChairUpdateModel model, string id)
    {
        if (ModelState.IsValid) 
        {
            var updateResult = await _chairService.UpdateChairAsync(model, id);
            if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
                return Ok(updateResult);

            else if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(updateResult.Message);
        }

        return BadRequest("Invalid fields.");
    }
}
