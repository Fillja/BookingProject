﻿using Infrastructure.Entities;
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
            var listResult = await _seatingRepository.GetAllWithIdAsync(tableId);

            if (listResult.StatusCode == StatusCode.OK)
            {
                var cleanList = await CreateCleanListAsync((IEnumerable<SeatingEntity>)listResult.Content!, (TableEntity)tableResult.Content!);
                return ResponseFactory.Ok(cleanList);
            }

            else if(listResult.StatusCode == StatusCode.NOT_FOUND)
                return ResponseFactory.NotFound(listResult.Message!);

            return ResponseFactory.BadRequest(listResult.Message!);

        }
        else if (tableResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound("Table could not be found.");

        return ResponseFactory.BadRequest(tableResult.Message!);
    }

    public async Task<List<object>> CreateCleanListAsync(IEnumerable<SeatingEntity> seatingList, TableEntity tableEntity)
    {
        List<object> cleanList = [];
        cleanList.Add(tableEntity);

        foreach(var seatingEntity in seatingList)
        {
            var chairResult = await _chairRepository.GetOneAsync(x => x.Id == seatingEntity.ChairId);

            if (chairResult.StatusCode == StatusCode.OK)
                cleanList.Add((ChairEntity)chairResult.Content!);
        }

        return cleanList;
    }
}
