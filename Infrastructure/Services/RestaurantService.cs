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
            return ResponseFactory.Exists("That entity already exists.");

        var restaurantEntity = new RestaurantEntity
        {
            Name = model.RestaurantName,
            Location = model.Location,
        };

        var createResult = await _restaurantRepository.CreateAsync(restaurantEntity);

        if (createResult.StatusCode == StatusCode.CREATED)
            return ResponseFactory.Created(createResult);

        return ResponseFactory.BadRequest();
    }

    public async Task<ResponseResult> UpdateRestaurantAsync(RestaurantModel model, string id)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode == StatusCode.OK)
        {
            var entityToUpdate = getResult.Content as RestaurantEntity;
            entityToUpdate!.Name = model.RestaurantName;
            entityToUpdate.Location = model.Location;

            var updateResult = await _restaurantRepository.UpdateAsync(entityToUpdate);

            if (updateResult.StatusCode == StatusCode.OK)
                return ResponseFactory.Ok(updateResult);
        }

        else if(getResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Entity was not found.");

        return ResponseFactory.BadRequest();
    }
}
