using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class TableChairService(TableChairRepository tableChairRepository)
{
    private readonly TableChairRepository _tableChairRepository = tableChairRepository;

    public async Task<ResponseResult> CreateCombinedTableAsync(TableChairsModel model)
    {
        var tableChairList = new List<TableChairEntity>();

        //ÄNDRA SÅ ATT DU HÄMTAR ENTITET HÄR ISTÄLLET - CHATGPT SVAR 

        foreach (var chair in model.Chairs)
        {
            var tableChairEntity = new TableChairEntity
            {
                Name = model.Name,
                Table = model.Table,
                TableId = model.Table.Id,
                Chair = chair,
                ChairId = chair.Id,
            };

            tableChairList.Add(tableChairEntity);
        }

        var createResult = await _tableChairRepository.CreateMultipleAsync(tableChairList);

        if (createResult.StatusCode == StatusCode.CREATED)
            return ResponseFactory.Created(createResult);

        return ResponseFactory.BadRequest(createResult.Message!);
    }
}
