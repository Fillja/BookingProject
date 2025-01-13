using Infrastructure.Entities;
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

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantModel model)
    {
        if (ModelState.IsValid) 
        {
            var createResult = await _restaurantService.CreateRestaurantAsync(model);

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
                return Created($"/api/restaurant/{createResult.Content}", createResult.Content);
            
            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.EXISTS)
                return Conflict();
        }

        return BadRequest();
    }
}
