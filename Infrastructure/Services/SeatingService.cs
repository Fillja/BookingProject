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

    public async Task<ResponseResult> CreateOneSeatingAsync(SeatingModel model, string chairId)
    {
        var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chairId);

        if (getChairResult.StatusCode == StatusCode.OK)
        {
            var seatingEntity = new SeatingEntity
            {
                Name = model.Name,
                TableId = model.Table.Id,
                ChairId = chairId,
            };

            var createResult = await _seatingRepository.CreateAsync(seatingEntity);

            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult.Message!);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        else if(getChairResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound(getChairResult.Message!);

        return ResponseFactory.BadRequest(getChairResult.Message!);
    }

    public async Task<ResponseResult> CreateSeatingAsync(SeatingCreateModel model)
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

    public async Task<ResponseResult> CreateSeatingListAsync(SeatingCreateModel model)
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
                    var seatingEntity = new SeatingEntity
                    {
                        Name = model.Name,
                        Table = (TableEntity)getTableResult.Content!,
                        TableId = model.TableId,
                        Chair = (ChairEntity)getChairResult.Content!,
                        ChairId = chairId
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

    //Instead of returning directly from repo with every instance of the table, we only return one instance of the table with its corresponding chairs
    public async Task<ResponseResult> GetOneSeatingAsync(string tableId)
    {
        var tableResult = await _tableRepository.GetOneAsync(x => x.Id == tableId);

        if (tableResult.StatusCode == StatusCode.OK)
        {
            var seatingListResult = await _seatingRepository.GetAllWithTableIdAsync(tableId);

            if (seatingListResult.StatusCode == StatusCode.OK)
            {
                var seatingModel = CreateSeatingModel((IEnumerable<SeatingEntity>)seatingListResult.Content!, (TableEntity)tableResult.Content!);
                return ResponseFactory.Ok(seatingModel);
            }

            else if(seatingListResult.StatusCode == StatusCode.NOT_FOUND)
                return ResponseFactory.NotFound(seatingListResult.Message!);

            return ResponseFactory.BadRequest(seatingListResult.Message!);

        }
        else if (tableResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Table could not be found.");

        return ResponseFactory.BadRequest(tableResult.Message!);
    }

    //Instead of returning directly from repo with every instance of the table, we only return one instance of the table with its corresponding chairs
    public async Task<ResponseResult> GetAllSeatingsAsync()
    {
        var listResult = await _seatingRepository.GetAllAsync();

        if (listResult.StatusCode == StatusCode.OK)
        {
            var seatingModelList = CreateSeatingModelList((IEnumerable<SeatingEntity>)listResult.Content!);
            return ResponseFactory.Ok(seatingModelList);

        }
        else if (listResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound(listResult.Message!);

        return ResponseFactory.BadRequest(listResult.Message!);
    }

    //Creates a single sorted seating model for the GetOneSeatingAsync() function
    public static SeatingModel CreateSeatingModel(IEnumerable<SeatingEntity> seatingList, TableEntity tableEntity)
    {
        var seatingModel = new SeatingModel
        {
            Name = seatingList.First().Name,
            Table = tableEntity,
        };

        foreach (var seatingEntity in seatingList)
        {
            seatingModel.Chairs.Add(seatingEntity.Chair);
        }

        return seatingModel;
    }

    //Creates a list of sorted seating models for the GetAllSeatingsAsync() function
    public static List<SeatingModel> CreateSeatingModelList(IEnumerable<SeatingEntity> seatingList)
    {
        var seatingModelList = seatingList
            .GroupBy(s => s.TableId)
            .Select(group => new SeatingModel
            {
                Table = group.First().Table,
                Name = group.First().Name,
                Chairs = group.Select(s => s.Chair).ToList()
            })
            .ToList();

        return seatingModelList;
    }
}
