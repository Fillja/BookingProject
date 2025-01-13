using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class RestaurantService(RestaurantRepository restaurantRepository)
{
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> CreateRestaurantAsync(RestaurantModel model)
    {
        var existResult = await _restaurantRepository.ExistsAsync(x => x.Name == model.RestaurantName);
        if (existResult.StatusCode == StatusCode.EXISTS)
            return ResponseFactory.Exists();

        var restaurantEntity = new RestaurantEntity
        {
            Name = model.RestaurantName,
            Location = model.Location,
        };

        var createResult = await _restaurantRepository.CreateAsync(restaurantEntity);

        if (createResult.StatusCode == StatusCode.OK)
            return ResponseFactory.Ok(createResult);

        return ResponseFactory.BadRequest();
    }
}
