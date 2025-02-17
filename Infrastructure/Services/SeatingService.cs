using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class SeatingService(SeatingRepository seatingRepository, TableRepository tableRepository, ChairRepository chairRepository)
{
    private readonly SeatingRepository _seatingRepository = seatingRepository;
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly ChairRepository _chairRepository = chairRepository;

    public async Task<ResponseResult> CreateSeatingAsync(SeatingModel model)
    {
        var seatingListResult = await CreateSeatingListAsync(model);

        if (seatingListResult.StatusCode == StatusCode.CREATED) 
        {
            var seatingList = (IEnumerable<SeatingEntity>)seatingListResult.Content!;
            var createResult = await _seatingRepository.CreateMultipleAsync(seatingList);

            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        return ResponseFactory.NotFound(seatingListResult.Message!);
    }

    public async Task<ResponseResult> CreateSeatingListAsync(SeatingModel model)
    {
        var seatingList = new List<SeatingEntity>();
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == model.TableId);

        if (getTableResult.StatusCode == StatusCode.OK)
        {
            foreach (var chairId in model.ChairIds) 
            { 
                var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chairId);

                if(getChairResult.StatusCode == StatusCode.OK)
                {
                    var chairEntity = (ChairEntity)getChairResult.Content!;

                    var seatingEntity = new SeatingEntity
                    {
                        Name = model.Name,
                        Table = (TableEntity)getTableResult.Content!,
                        TableId = model.TableId,
                        Chair = chairEntity,
                        ChairId = chairEntity.Id
                    };

                    seatingList.Add(seatingEntity);
 
                }
                else if(getChairResult.StatusCode  == StatusCode.NOT_FOUND)
                    return ResponseFactory.NotFound(getChairResult.Message!);
            }
            return ResponseFactory.Created(seatingList);
        }
        return ResponseFactory.NotFound(getTableResult.Message!);
    }

    public async Task<ResponseResult> GetOneSeatingAsync(string tableId)
    {
        var tableResult = await _tableRepository.GetOneAsync(x => x.Id == tableId);

        if (tableResult.StatusCode == StatusCode.OK)
        {
            var seatingListResult = await _seatingRepository.GetAllWithTableIdAsync(tableId);

            if (seatingListResult.StatusCode == StatusCode.OK)
            {
                var sortedSeatingList = await CreateSortedSeatingListAsync((IEnumerable<SeatingEntity>)seatingListResult.Content!, (TableEntity)tableResult.Content!);
                return ResponseFactory.Ok(sortedSeatingList);
            }

            else if(seatingListResult.StatusCode == StatusCode.NOT_FOUND)
                return ResponseFactory.NotFound(seatingListResult.Message!);

            return ResponseFactory.BadRequest(seatingListResult.Message!);

        }
        else if (tableResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Table could not be found.");

        return ResponseFactory.BadRequest(tableResult.Message!);
    }

    public async Task<List<object>> CreateSortedSeatingListAsync(IEnumerable<SeatingEntity> seatingList, TableEntity tableEntity)
    {
        List<object> sortedSeatingList = [];
        sortedSeatingList.Add(tableEntity);

        foreach(var seatingEntity in seatingList)
        {
            var chairResult = await _chairRepository.GetOneAsync(x => x.Id == seatingEntity.ChairId);

            if (chairResult.StatusCode == StatusCode.OK)
                sortedSeatingList.Add((ChairEntity)chairResult.Content!);
        }

        return sortedSeatingList;
    }
}
