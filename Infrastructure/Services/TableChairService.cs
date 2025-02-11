using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class TableChairService(TableChairRepository tableChairRepository, TableRepository tableRepository, ChairRepository chairRepository)
{
    private readonly TableChairRepository _tableChairRepository = tableChairRepository;
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly ChairRepository _chairRepository = chairRepository;

    public async Task<ResponseResult> CreateCombinedTableAsync(TableChairsModel model)
    {
        var listResult = await CreateTableChairListAsync(model);
        if (listResult.StatusCode == StatusCode.CREATED) 
        {
            var tableChairList = (IEnumerable<TableChairEntity>)listResult.Content!;
            var createResult = await _tableChairRepository.CreateMultipleAsync(tableChairList);

            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        return ResponseFactory.NotFound(listResult.Message!);
    }

    public async Task<ResponseResult> CreateTableChairListAsync(TableChairsModel model)
    {
        var tableChairList = new List<TableChairEntity>();
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == model.TableId);

        if (getTableResult.StatusCode == StatusCode.OK)
        {
            var tableEntity = (TableEntity)getTableResult.Content!;

            foreach (var chairId in model.ChairIds) 
            { 
                var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chairId);

                if(getChairResult.StatusCode == StatusCode.OK)
                {
                    var chairEntity = (ChairEntity)getChairResult.Content!;

                    var tableChairEntity = new TableChairEntity
                    {
                        Name = model.Name,
                        Table = tableEntity,
                        TableId = model.TableId,
                        Chair = chairEntity,
                        ChairId = chairEntity.Id
                    };

                    tableChairList.Add(tableChairEntity);
                    
                   
                }
                else if(getChairResult.StatusCode  == StatusCode.NOT_FOUND)
                    return ResponseFactory.NotFound(getChairResult.Message!);
            }
            return ResponseFactory.Created(tableChairList);
        }
        return ResponseFactory.NotFound(getTableResult.Message!);
    }
}
