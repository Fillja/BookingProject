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
}