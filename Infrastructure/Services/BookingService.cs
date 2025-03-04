using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BookingService(BookingRepository bookingRepository, SeatingRepository seatingRepository, ChairRepository chairRepository, TableRepository tableRepository, SeatingService seatingService)
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly SeatingRepository _seatingRepository = seatingRepository;
    private readonly ChairRepository _chairRepository = chairRepository;
    private readonly TableRepository _tableRepository = tableRepository;
    private readonly SeatingService _seatingService = seatingService;

    public async Task<ResponseResult> CreateBookingAsync(BookingCreateModel bookingCreateModel, SeatingBookingModel seatingBookingModel)
    {
        var getSeatingResult = await _seatingRepository.GetOneAsync(x => x.TableId == seatingBookingModel.TableId);
        if(getSeatingResult.HasFailed)
            return getSeatingResult;

        var bookingResult = await BookTableAndChairAsync(seatingBookingModel);
        if(bookingResult.HasFailed)
            return bookingResult;

        var bookingEntity = EntityFactory.PopulateBookingEntity((SeatingEntity)getSeatingResult.Content!, bookingCreateModel);
        var createResult = await _bookingRepository.CreateAsync(bookingEntity);
        return createResult;

    }

    public async Task<ResponseResult> BookTableAndChairAsync(SeatingBookingModel seatingBookingModel)
    {
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == seatingBookingModel.TableId);
        if (getTableResult.HasFailed)
            return getTableResult;

        foreach (var chair in seatingBookingModel.Chairs) 
        {
            var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chair.ChairId);
            if (getChairResult.HasFailed)
                return getChairResult;
                
            var chairEntity = EntityFactory.PopulateChairEntity((ChairEntity)getChairResult.Content!, chair);

            var updateResult = await _chairRepository.UpdateAsync(chairEntity);
            if (updateResult.HasFailed)
                return updateResult;
        }

        var tableEntity = (TableEntity)getTableResult.Content!;
        tableEntity.IsBooked = true;

        var updateTableResult = await _tableRepository.UpdateAsync(tableEntity);
        if (updateTableResult.HasFailed)
            return updateTableResult;

        return ResponseResult.Result(0);
    }

    public async Task<ResponseResult> GetOneBookingAsync(string id)
    {
        var getBookingResult = await _bookingRepository.GetOneAsync(x => x.Id == id);
        if(getBookingResult.HasFailed)
            return getBookingResult;

        var bookingEntity = (BookingEntity)getBookingResult.Content!;

        var getSeatingResult = await _seatingService.GetOneSeatingAsync(bookingEntity.Seating!.TableId);
        if (getSeatingResult.HasFailed)
            return getSeatingResult;

        var seatingModel = (SeatingModel)getSeatingResult.Content!;

        var completeBooking = EntityFactory.PopulateBookingModel(bookingEntity, seatingModel);

        return ResponseResult.Result(0, "", completeBooking);
    }
}
