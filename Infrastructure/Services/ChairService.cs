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

    public async Task<ResponseResult> CreateChairAsync(ChairModel chairModel)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == chairModel.RestaurantName!.ToLower());
        if(HttpErrorHandler.HasHttpError(getResult))
            return getResult;

        var chairEntity = PopulateChairEntity((RestaurantEntity)getResult.Content!, chairModel);
        var createResult = await _chairRepository.CreateAsync(chairEntity);
        return createResult;

    }

    public async Task<ResponseResult> UpdateChairAsync(ChairUpdateModel chairUpdateModel)
    {
        var getResult = await _chairRepository.GetOneAsync(x => x.Id == chairUpdateModel.ChairId);
        if(HttpErrorHandler.HasHttpError(getResult))
            return getResult;

        var entityToUpdate = PopulateChairEntity((ChairEntity)getResult.Content!, chairUpdateModel);
        var updateResult = await _chairRepository.UpdateAsync(entityToUpdate);
        return updateResult;

    }

    public ChairEntity PopulateChairEntity(RestaurantEntity restaurantEntity, ChairModel chairModel)
    {
        return (new ChairEntity
        {
            Name = chairModel.Name,
            Restaurant = restaurantEntity,
            RestaurantId = restaurantEntity.Id,
            Vegan = chairModel.Vegan,
            Vegetarian = chairModel.Vegetarian,
            Gluten = chairModel.Gluten,
            Milk = chairModel.Milk,
            Eggs = chairModel.Egg,
        });
    }

    public ChairEntity PopulateChairEntity(ChairEntity chairEntity, ChairUpdateModel chairUpdateModel)
    {
        chairEntity.Name = chairUpdateModel.Name;
        chairEntity.Vegan = chairUpdateModel.Vegan;
        chairEntity.Vegetarian = chairUpdateModel.Vegetarian;
        chairEntity.Gluten = chairUpdateModel.Gluten;
        chairEntity.Milk = chairUpdateModel.Milk;
        chairEntity.Eggs = chairUpdateModel.Egg;

        return chairEntity;
    }
}