using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class RestaurantService(RestaurantRepository restaurantRepository) : IRestaurantService
{
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> GetAllRestaurantsAsync()
    {
        ResponseResult listResult = await _restaurantRepository.GetAllAsync();
        if (listResult.HasFailed)
            return listResult;

        IEnumerable<RestaurantEntity> restaurantList = (IEnumerable<RestaurantEntity>)listResult.Content!;
        IEnumerable<RestaurantModel> restaurantModelList = restaurantList.Select(restaurant => EntityFactory.PopulateRestaurantModel(restaurant));

        return new ResponseResult { StatusCode = 0, Message = listResult.Message!, Content = restaurantModelList };
    }

    public async Task<ResponseResult> GetOneRestaurantAsync(string restaurantId)
    {
        ResponseResult getResult = await _restaurantRepository.GetOneAsync(x => x.Id == restaurantId);
        if (getResult.HasFailed)
            return getResult;

        RestaurantEntity restaurantEntity = (RestaurantEntity)getResult.Content!;
        RestaurantModel restaurantModel = EntityFactory.PopulateRestaurantModel(restaurantEntity);

        return ResponseResult.Result(0, getResult.Message!, restaurantModel);
    }
}
