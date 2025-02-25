using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ChairService(ChairRepository chairRepository, RestaurantRepository restaurantRepository)
{
    private readonly ChairRepository _chairRepository = chairRepository;
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> CreateChairAsync(ChairModel model)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == model.RestaurantName!.ToLower());

        if (getResult.StatusCode == StatusCode.OK)
        {
            var restaurantEntity = getResult.Content as RestaurantEntity;
            var chairEntity = new ChairEntity
            {
                Name = model.Name,
                Restaurant = restaurantEntity!,
                Vegan = model.Vegan,
                Vegetarian = model.Vegetarian,
                Gluten = model.Gluten,
                Milk = model.Milk,
                Eggs = model.Egg,
            };
            var createResult = await _chairRepository.CreateAsync(chairEntity);

            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        else if (getResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Restaurant could not be found.");

        return ResponseFactory.BadRequest();
    }

    public async Task<ResponseResult> UpdateChairAsync(ChairUpdateModel model)
    {
        var getResult = await _chairRepository.GetOneAsync(x => x.Id == model.ChairId);

        if (getResult.StatusCode == StatusCode.OK)
        {
            var entityToUpdate = getResult.Content as ChairEntity;
            entityToUpdate!.Name = model.Name;
            entityToUpdate.Vegan = model.Vegan;
            entityToUpdate.Vegetarian = model.Vegetarian;
            entityToUpdate.Eggs = model.Egg;
            entityToUpdate.Milk = model.Milk;
            entityToUpdate.Gluten = model.Gluten;

            var updateResult = await _chairRepository.UpdateAsync(entityToUpdate);

            if (updateResult.StatusCode == StatusCode.OK)
                return ResponseFactory.Ok(updateResult);

            return ResponseFactory.BadRequest(updateResult.Message!);
        }

        else if (getResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound(getResult.Message!);

        return ResponseFactory.BadRequest();
    }
}