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

    //Creates a COMPLETE seating with several chairs - A list of SeatingEntities
    public async Task<ResponseResult> CreateCompleteSeatingAsync(SeatingCreateModel model)
    {
        var seatingList = new List<SeatingEntity>();

        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == model.TableId);
        if (getTableResult.HasFailed)
            return getTableResult;

        foreach (var chairId in model.ChairIds)
        {
            var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chairId);
            if (getTableResult.HasFailed)
                return getChairResult;

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

        var createResult = await _seatingRepository.CreateMultipleAsync(seatingList);
        return createResult;
    }

    //Creates a SINGLE SeatingEntity - Currently used for adding one chair to an already existing Seating
    public async Task<ResponseResult> CreateSingleSeatingAsync(SeatingSingleModel model)
    {
        var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == model.ChairId);

        if (getChairResult.HasFailed)
            return getChairResult;

        var seatingEntity = new SeatingEntity
        {
            TableId = model.TableId,
            ChairId = model.ChairId,
        };

        var createResult = await _seatingRepository.CreateAsync(seatingEntity);
        return createResult;
    }


    //Instead of returning directly from repo with every instance of the table, we only return one instance of the table with its corresponding chairs
    public async Task<ResponseResult> GetOneSeatingAsync(string tableId)
    {
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == tableId);
        if(getTableResult.HasFailed)
            return getTableResult;

        var seatingListResult = await _seatingRepository.GetAllWithTableIdAsync(tableId);
        if(seatingListResult.HasFailed)
            return seatingListResult;


        var seatingList = (IEnumerable<SeatingEntity>)seatingListResult.Content!;
        var seatingModel = new SeatingModel
        {
            Name = seatingList.First().Name,
            Table = (TableEntity)getTableResult.Content!
        };

        foreach (var seatingEntity in seatingList)
        {
            seatingModel.Chairs.Add(seatingEntity.Chair);
        }

        return ResponseResult.Result(0, "", seatingModel);
    }


    //Instead of returning directly from repo with every instance of the table, we returna list with only one instance of the table with its corresponding chairs per item in list
    public async Task<ResponseResult> GetAllSeatingsAsync(string restaurantId)
    {
        var listResult = await _seatingRepository.GetAllAsync(restaurantId);
        if(listResult.HasFailed)
            return listResult!;

        var seatingList = (IEnumerable<SeatingEntity>)listResult.Content!;
        var seatingModelList = seatingList
            .GroupBy(s => s.TableId)
            .Select(group => new SeatingModel
            {
                Table = group.First().Table,
                Name = group.First().Name,
                Chairs = group.Select(s => s.Chair).ToList()
            })
            .ToList();

        return ResponseResult.Result(0, "", seatingModelList);
    }
}
