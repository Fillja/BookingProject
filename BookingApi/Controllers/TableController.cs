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

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"api/table/create/{createResult}", createResult.Content);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _tableRepository.GetAllAsync();

        if (listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult.Content);

        else if (listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getResult.Content);

        else if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message); 
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, TableUpdateModel model)
    {
        var updateResult = await _tableService.UpdateTableAsync(id, model);

        if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(updateResult.Content);

        else if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(updateResult.Message);

        return BadRequest(updateResult.Message);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _tableRepository.DeleteAsync(x => x.Id == id);
        
        if(deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(deleteResult.Message);

        else if(deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}