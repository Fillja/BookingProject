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
        var listResult = await CreateSeatingListAsync(model);

        if (listResult.StatusCode == StatusCode.CREATED) 
        {
            var seatingList = (IEnumerable<SeatingEntity>)listResult.Content!;
            var createResult = await _seatingRepository.CreateMultipleAsync(seatingList);

            if (createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        return ResponseFactory.NotFound(listResult.Message!);
    }

    public async Task<ResponseResult> CreateSeatingListAsync(SeatingModel model)
    {
        var seatingList = new List<SeatingEntity>();
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

                    var seatingEntity = new SeatingEntity
                    {
                        Name = model.Name,
                        Table = tableEntity,
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
}
