using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class RestaurantService(RestaurantRepository restaurantRepository)
{
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> GetAllRestaurantsAsync()
    {
        var listResult = await _restaurantRepository.GetAllAsync();
        if (listResult.HasFailed)
            return listResult;

        var restaurantList = (IEnumerable<RestaurantEntity>)listResult.Content!;
        var restaurantModelList = restaurantList.Select(restaurant => EntityFactory.PopulateRestaurantModel(restaurant));

        return new ResponseResult{StatusCode = 0, Message = listResult.Message!, Content = restaurantModelList};
    }

    public async Task<ResponseResult> GetOneRestaurantAsync(string restaurantId)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Id == restaurantId);
        if (getResult.HasFailed)
            return getResult;

        var restaurantEntity = (RestaurantEntity)getResult.Content!;
        var restaurantModel = EntityFactory.PopulateRestaurantModel(restaurantEntity);

        return ResponseResult.Result(0, getResult.Message!, restaurantModel);
    }
}
