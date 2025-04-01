using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class TableService(TableRepository tableRepository, RestaurantRepository restaurantRepository) : ITableService
{
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly RestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<ResponseResult> CreateTableAsync(TableModel tableModel)
    {
        var getRestaurantResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == tableModel.RestaurantName!.ToLower());
        if (getRestaurantResult.HasFailed)
        {
            if (getRestaurantResult.StatusCode == 2)
                getRestaurantResult.Message = "Restaurant could not be found";

            return getRestaurantResult;
        }

        var tableEntity = EntityFactory.PopulateTableEntity((RestaurantEntity)getRestaurantResult.Content!, tableModel);
        var createResult = await _tableRepository.CreateAsync(tableEntity);
        return createResult;

    }

    public async Task<ResponseResult> UpdateTableAsync(string id, TableModel tableModel)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);
        if (getResult.HasFailed)
            return getResult;

        var entityToUpdate = EntityFactory.PopulateTableEntity((TableEntity)getResult.Content!, tableModel);
        var updateResult = await _tableRepository.UpdateAsync(entityToUpdate);
        return updateResult;
    }
}