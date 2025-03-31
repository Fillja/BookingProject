using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;


/*
    RESTAURANT MANAGMENT IS DEVELOPER-ONLY & IS WITHHELD TO SQL.
    API FOR RESTAURANT IS READ-ONLY
*/

//public class RestaurantService(RestaurantRepository restaurantRepository)
//{
//    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

//    public async Task<ResponseResult> CreateRestaurantAsync(RestaurantModel model)
//    {
//        var existResult = await _restaurantRepository.ExistsAsync(x => x.Name == model.RestaurantName);
//        if (existResult.HasFailed)
//            return existResult;

//        var restaurantEntity = new RestaurantEntity
//        {
//            Name = model.RestaurantName,
//            Location = model.Location,
//        };

//        var createResult = await _restaurantRepository.CreateAsync(restaurantEntity);
//        return createResult;
//    }

//    public async Task<ResponseResult> UpdateRestaurantAsync(RestaurantModel model, string id)
//    {
//        var getResult = await _restaurantRepository.GetOneAsync(x => x.Id == id);
//        if (getResult.HasFailed)
//            return getResult;

//        var entityToUpdate = (RestaurantEntity)getResult.Content!;
//        entityToUpdate.Name = model.RestaurantName;
//        entityToUpdate.Location = model.Location;

//        var updateResult = await _restaurantRepository.UpdateAsync(entityToUpdate);
//        return updateResult;
//    }
//}