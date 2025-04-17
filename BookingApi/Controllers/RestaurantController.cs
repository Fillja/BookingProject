using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    private readonly IRestaurantService _restaurantService = restaurantService;

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _restaurantService.GetAllRestaurantsAsync();

        if (listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if (listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getResult = await _restaurantService.GetOneRestaurantAsync(id);

        if (getResult.StatusCode.Equals(0))
            return Ok(getResult.Content);

        else if (getResult.StatusCode.Equals(2))
            return NotFound(getResult.Message);

        return BadRequest(getResult.Message);
    }
}