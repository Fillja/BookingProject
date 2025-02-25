using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BookingService(BookingRepository bookingRepository, SeatingRepository seatingRepository, ChairRepository chairRepository, TableRepository tableRepository)
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly SeatingRepository _seatingRepository = seatingRepository;
    private readonly ChairRepository _chairRepository = chairRepository;
    private readonly TableRepository _tableRepository = tableRepository;

    public async Task<ResponseResult> CreateBookingAsync(BookingModel bookingModel, SeatingBookingModel seatingBookingModel)
    {
        var getSeatingResult = await _seatingRepository.GetOneAsync(x => x.TableId == seatingBookingModel.TableId);
        if(HttpErrorHandler.HasHttpError(getSeatingResult))
            return getSeatingResult;

        var bookingEntity = PopulateBookingEntity((SeatingEntity)getSeatingResult.Content!, bookingModel);

        var bookingResult = await BookTableAndChair(seatingBookingModel);
        if(HttpErrorHandler.HasHttpError(bookingResult))
            return bookingResult;

        var createResult = await _bookingRepository.CreateAsync(bookingEntity);
        return createResult;

    }

    public async Task<ResponseResult> BookTableAndChair(SeatingBookingModel seatingBookingModel)
    {
        var getTableResult = await _tableRepository.GetOneAsync(x => x.Id == seatingBookingModel.TableId);
        if (HttpErrorHandler.HasHttpError(getTableResult))
            return getTableResult;

        var tableEntity = (TableEntity)getTableResult.Content!;
        tableEntity.IsBooked = true;
        
        var updateTableResult = await _tableRepository.UpdateAsync(tableEntity);
        if (HttpErrorHandler.HasHttpError(updateTableResult))
            return updateTableResult;

        foreach (var chair in seatingBookingModel.Chairs) 
        {
            var getChairResult = await _chairRepository.GetOneAsync(x => x.Id == chair.ChairId);
            if (HttpErrorHandler.HasHttpError(getChairResult))
                return getChairResult;
                
            var chairEntity = PopulateChairEntity((ChairEntity)getChairResult.Content!, chair);

            var updateResult = await _chairRepository.UpdateAsync(chairEntity);
            if (HttpErrorHandler.HasHttpError(updateResult))
                return updateResult;
        }

        return ResponseFactory.Ok();
    }

    public BookingEntity PopulateBookingEntity(SeatingEntity seatingEntity, BookingModel bookingModel) 
    {
        return(new BookingEntity
        {
            BookingStartTime = bookingModel.BookingStartTime,
            BookingEndTime = bookingModel.BookingStartTime.AddHours(6),
            BookerName = bookingModel.BookerName,
            BookerEmail = bookingModel.BookerEmail,
            BookerPhone = bookingModel.BookerPhone,
            SpecialRequests = bookingModel.SpecialRequests,
            SeatingId = seatingEntity.Id,
            Seating = seatingEntity
        });
    }

    public ChairEntity PopulateChairEntity(ChairEntity chairEntity, ChairUpdateModel chair) 
    {
        chairEntity.Vegan = chair.Vegan;
        chairEntity.Vegetarian = chair.Vegetarian;
        chairEntity.Gluten = chair.Gluten;
        chairEntity.Milk = chair.Milk;
        chairEntity.Eggs = chair.Egg;

        return chairEntity;
    }

}
