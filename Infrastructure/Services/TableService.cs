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

    public async Task<ResponseResult> CreateTableAsync(TableModel tableModel)
    {
        var getRestaurantResult = await _restaurantRepository.GetOneAsync(x => x.Id == tableModel.RestaurantId);
        if (getRestaurantResult.HasFailed)
        {
            if (getRestaurantResult.StatusCode == 2)
                getRestaurantResult.Message = "Restaurant could not be found";

            return getRestaurantResult;
        }

        var tableEntity = EntityFactory.PopulateTableEntity((RestaurantEntity)getRestaurantResult.Content!, tableModel);
        var createResult = await _tableRepository.CreateAsync(tableEntity);
        if(createResult.HasFailed)
            return createResult;

        tableModel.Id = tableEntity.Id;
        return ResponseResult.Result(0, createResult.Message!, tableModel);

    }

    public async Task<ResponseResult> GetAllTablesWithBookingsAsync(string restaurantId)
    {
        var listResult = await _tableRepository.GetAllAsync(restaurantId);
        if (listResult.HasFailed)
            return listResult;

        var tableList = (IEnumerable<TableEntity>)listResult.Content!;
        var tableModelList = tableList.Select(table => EntityFactory.PopulateTableModel(table)).ToList();

        return ResponseResult.Result(0, listResult.Message!, tableModelList);
    }


    public async Task<ResponseResult> GetTableAsync(string id)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);
        if (getResult.HasFailed)
            return getResult;

        var tableEntity = (TableEntity)getResult.Content!;
        var tableModel = EntityFactory.PopulateTableModel(tableEntity);

        return ResponseResult.Result(0, getResult.Message!, tableModel);
    }

    public async Task<ResponseResult> UpdateTableAsync(string id, TableModel tableModel)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);
        if (getResult.HasFailed)
            return getResult;

        var entityToUpdate = EntityFactory.PopulateTableEntity((TableEntity)getResult.Content!, tableModel);
        var updateResult = await _tableRepository.UpdateAsync(entityToUpdate);
        if(updateResult.HasFailed)
            return updateResult;
        
        var updatedTableModel = EntityFactory.PopulateTableModel((TableEntity)updateResult.Content!);
        return ResponseResult.Result(0, updateResult.Message!, updatedTableModel);
    }
}