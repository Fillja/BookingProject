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

            else if(createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }
}
