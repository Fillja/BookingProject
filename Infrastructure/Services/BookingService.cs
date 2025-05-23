﻿using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BookingService(BookingRepository bookingRepository, TableRepository tableRepository) : IBookingService
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly TableRepository _tableRepository = tableRepository;

    public async Task<ResponseResult> CreateBookingAsync(BookingModel bookingModel)
    {
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == bookingModel.TableId);
        if (getTableResult.HasFailed)
            return getTableResult;

        var tableEntity = (TableEntity)getTableResult.Content!;
        if (tableEntity.IsBookedAt(bookingModel.BookingStartTime, bookingModel.BookingEndTime))
            return ResponseResult.Result(3, "Table is already booked for the selected time.");

        var bookingEntity = EntityFactory.PopulateBookingEntity(bookingModel, (TableEntity)getTableResult.Content!);
        var createResult = await _bookingRepository.CreateAsync(bookingEntity);
        if (createResult.HasFailed)
            return createResult;

        bookingModel.Id = bookingEntity.Id;
        return ResponseResult.Result(0, createResult.Message!, bookingModel);

    }

    public async Task<ResponseResult> GetAllBookingsAsync(string restaurantId)
    {
        var listResult = await _bookingRepository.GetAllAsync(restaurantId);
        if (listResult.HasFailed)
            return listResult;

        var bookingList = (IEnumerable<BookingEntity>)listResult.Content!;
        var bookingModelList = bookingList.Select(booking => EntityFactory.PopulateBookingModel(booking)).ToList();

        return ResponseResult.Result(0, listResult.Message!, bookingModelList);
    }

    public async Task<ResponseResult> GetBookingAsync(string id)
    {
        var getResult = await _bookingRepository.GetOneAsync(x => x.Id == id);
        if (getResult.HasFailed)
            return getResult;

        var bookingEntity = (BookingEntity)getResult.Content!;
        var bookingModel = EntityFactory.PopulateBookingModel(bookingEntity);

        return ResponseResult.Result(0, getResult.Message!, bookingModel);
    }

    public async Task<ResponseResult> UpdateBookingAsync(string id, BookingModel bookingModel)
    {
        var getResult = await _bookingRepository.GetOneAsync(x => x.Id == id);
        if (getResult.HasFailed)
            return getResult;

        var entityToUpdate = EntityFactory.PopulateBookingEntity((BookingEntity)getResult.Content!, bookingModel);
        var updateResult = await _bookingRepository.UpdateAsync(entityToUpdate);
        if (updateResult.HasFailed)
            return updateResult;

        var updatedBookingModel = EntityFactory.PopulateBookingModel((BookingEntity)updateResult.Content!);
        return ResponseResult.Result(0, updateResult.Message!, updatedBookingModel);
    }
}
