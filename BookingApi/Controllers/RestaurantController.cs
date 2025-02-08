using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController(RestaurantRepository restaurantRepository, RestaurantService restaurantService) : ControllerBase
{
    private readonly RestaurantService _restaurantService = restaurantService;
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    [HttpPost("create")]
    public async Task<IActionResult> Create(RestaurantModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _restaurantService.CreateRestaurantAsync(model);

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"/api/restaurant/create/{createResult.Content}", createResult);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.EXISTS)
                return Conflict(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _restaurantRepository.GetAllAsync();

        if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult);

        else if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Id == id);

        if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getResult);

        else if(getResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(RestaurantModel model, string id)
    {
        if (ModelState.IsValid) 
        {
            var updateResult = await _restaurantService.UpdateRestaurantAsync(model, id);

            if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
                return Ok(updateResult);
            
            else if(updateResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(updateResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _restaurantRepository.DeleteAsync(x  => x.Id == id);

        if (deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(deleteResult.Message);

        else if (deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
