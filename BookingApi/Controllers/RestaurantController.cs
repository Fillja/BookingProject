using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController(RestaurantRepository restaurantRepository) : ControllerBase
{
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    /*
    ADMINISTRATING RESTAURANTS SHOULD NOT BE EXPOSED IN ENDPOINTS - WILL INSTEAD BE MANAGED BY DEVELOPER IN SQL

    [HttpPost("create")]
    public async Task<IActionResult> Create(RestaurantModel model)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _restaurantService.CreateRestaurantAsync(model);

            if (createResult.StatusCode.Equals(0))
                return Created($"/api/restaurant/create/{createResult.Content}", createResult.Content);

            else if (createResult.StatusCode.Equals(3))
                return Conflict(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }
    */

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _restaurantRepository.GetAllAsync();

        if (listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if (listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode.Equals(0))
            return Ok(getResult.Content);

        else if (getResult.StatusCode.Equals(2))
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message);
    }

    /*
    ADMINISTRATING RESTAURANTS SHOULD NOT BE EXPOSED IN ENDPOINTS - WILL INSTEAD BE MANAGED BY DEVELOPER IN SQL

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(RestaurantModel model, string id)
    {
        if (ModelState.IsValid)
        {
            var updateResult = await _restaurantService.UpdateRestaurantAsync(model, id);

            if (updateResult.StatusCode.Equals(0))
                return Ok(updateResult.Content);

            else if (updateResult.StatusCode.Equals(2))
                return NotFound(updateResult.Message);

            return BadRequest(updateResult.Message);
        }

        return BadRequest("Invalid fields.");
    }
    */

    /*
    ADMINISTRATING RESTAURANTS SHOULD NOT BE EXPOSED IN ENDPOINTS - WILL INSTEAD BE MANAGED BY DEVELOPER IN SQL
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _restaurantRepository.DeleteAsync(x => x.Id == id);

        if (deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        else if (deleteResult.StatusCode.Equals(2))
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
    */
}