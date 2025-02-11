using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TableChairController(TableChairRepository tableChairRepository, TableChairService tableChairService) : ControllerBase
{
    private readonly TableChairRepository _tableChairRepository = tableChairRepository;
    private readonly TableChairService _tableChairService = tableChairService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(TableChairsModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _tableChairService.CreateCombinedTableAsync(model);

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"/api/tablechair/create/{createResult.Content}", createResult);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message!);

            return BadRequest(createResult.Message!);
        }

        return BadRequest("Invalid fields.");
    }
}
