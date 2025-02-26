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
        var getResult = await _restaurantRepository.GetOneAsync(x => x.Name.ToLower() == tableModel.RestaurantName!.ToLower());
        if (HttpErrorHandler.HasHttpError(getResult))
            return getResult;

        var tableEntity = PopulateTableEntity((RestaurantEntity)getResult.Content!, tableModel);
        var createResult = await _tableRepository.CreateAsync(tableEntity);
        return createResult;

    }

    public async Task<ResponseResult> UpdateTableAsync(string id, TableUpdateModel tableUpdateModel)
    {
        var getResult = await _tableRepository.GetOneAsync(x => x.Id == id);
        if (HttpErrorHandler.HasHttpError(getResult))
            return getResult;

        var entityToUpdate = PopulateTableEntity((TableEntity)getResult.Content!, tableUpdateModel);
        var updateResult = await _tableRepository.UpdateAsync(entityToUpdate);
        return updateResult;
    }

    public TableEntity PopulateTableEntity(RestaurantEntity restaurantEntity, TableModel tableModel)
    {
        return (new TableEntity
        {
            Name = tableModel.Name,
            Size = tableModel.Size,
            IsBooked = tableModel.IsBooked,
            Restaurant = restaurantEntity,
            RestaurantId = restaurantEntity.Id,
        });
    }

    public TableEntity PopulateTableEntity(TableEntity tableEntity, TableUpdateModel tableUpdateModel)
    {
        tableEntity.Name = tableUpdateModel.Name;
        tableEntity.Size = tableUpdateModel.Size;
        tableEntity.IsBooked = tableUpdateModel.IsBooked;

        return tableEntity;
    }
}