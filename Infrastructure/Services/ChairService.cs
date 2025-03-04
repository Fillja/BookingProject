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
        var getRestaurantResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == chairModel.RestaurantName!.ToLower());
        if (getRestaurantResult.HasFailed)
        {
            if (getRestaurantResult.StatusCode == 2)
                getRestaurantResult.Message = "Restaurant could not be found";

            return getRestaurantResult;
        }

        var chairEntity = EntityFactory.PopulateChairEntity((RestaurantEntity)getRestaurantResult.Content!, chairModel);
        var createResult = await _chairRepository.CreateAsync(chairEntity);
        return createResult;

    }

    public async Task<ResponseResult> UpdateChairAsync(string id, ChairModel chairModel)
    {
        var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == id);
        if(getChairResult.HasFailed)
            return getChairResult;

        var entityToUpdate = EntityFactory.PopulateChairEntity((ChairEntity)getChairResult.Content!, chairModel);
        var updateResult = await _chairRepository.UpdateAsync(entityToUpdate);
        return updateResult;

    }
}