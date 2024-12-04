using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController(RestaurantRepository restaurantRepository) : ControllerBase
{
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantModel model)
    {
        //INTE KLAR, FLYTTA TILL SERVICE
        if (ModelState.IsValid) 
        {
            var restaurantEntity = new RestaurantEntity
            {
                Name = model.RestaurantName,
                Location = model.Location,
            };

            var createResult = await _restaurantRepository.CreateAsync(restaurantEntity);

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
                return Created($"/api/restaurant/{createResult.Content}", createResult.Content);
            
            //FIXA SERVICE, FINNS INTE EXISTS I CREATERESULT.
            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.EXISTS)
                return Conflict();
        }

        return BadRequest();
    }
}
