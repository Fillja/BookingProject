using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TableController(TableRepository tableRepository, TableService tableService) : ControllerBase
{
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly TableService _tableService = tableService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(TableModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _tableService.CreateTableAsync(model);

            if (createResult.StatusCode.Equals(0))
                return Created($"api/table/create/{createResult}", createResult.Content);

            else if (createResult.StatusCode.Equals(2))
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall/{restaurantId}")]
    public async Task<IActionResult> GetAll(string restaurantId)
    {
        var listResult = await _tableRepository.GetAllAsync(restaurantId);

        if (listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if (listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode.Equals(0))
            return Ok(getResult.Content);

        else if(getResult.StatusCode.Equals(2))
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message); 
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, TableModel model)
    {
        if (ModelState.IsValid)
        {
            var updateResult = await _tableService.UpdateTableAsync(id, model);

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
        var deleteResult = await _tableRepository.DeleteAsync(x => x.Id == id);
        
        if(deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        else if(deleteResult.StatusCode.Equals(2))
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}