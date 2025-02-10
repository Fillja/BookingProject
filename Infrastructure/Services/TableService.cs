using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class TableService(TableRepository tableRepository, RestaurantRepository restaurantRepository)
{
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> CreateTableAsync(TableModel model)
    {
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == model.RestaurantName!.ToLower());
        if (getResult.StatusCode == StatusCode.OK)
        {
            var restaurantEntity = (RestaurantEntity)getResult.Content!;

            var tableEntity = new TableEntity
            {
                Name = model.Name,
                Size = model.Size,
                IsBooked = model.IsBooked,
                Restaurant = restaurantEntity,
                RestaurantId = restaurantEntity.Id,
            };
            var createResult = await _tableRepository.CreateAsync(tableEntity);
            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);

        }
        else if (getResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Restaurant could not be found.");

        return ResponseFactory.BadRequest(getResult.Message!);
    }

    public async Task<ResponseResult> UpdateTableAsync(string id, TableUpdateModel model)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);

        if (getResult.StatusCode == StatusCode.OK) 
        {
            var entityToUpdate = (TableEntity)getResult.Content!;
            entityToUpdate.Name = model.Name;
            entityToUpdate.Size = model.Size;
            entityToUpdate.IsBooked = model.IsBooked;

            var updateResult = await _tableRepository.UpdateAsync(entityToUpdate);

            if(updateResult.StatusCode == StatusCode.OK)
                return ResponseFactory.Ok(updateResult);

            return ResponseFactory.BadRequest(updateResult.Message!);
        }
        else if(getResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound(getResult.Message!);

        return ResponseFactory.BadRequest(getResult.Message!);
    }
}