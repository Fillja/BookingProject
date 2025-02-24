using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BookingService(BookingRepository bookingRepository, SeatingRepository seatingRepository)
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly SeatingRepository _seatingRepository = seatingRepository;

    public async Task<ResponseResult> CreateBookingAsync(BookingModel model)
    {
        var getSeatingResult = await _seatingRepository.GetOneAsync(x => x.Id == model.SeatingId);

        if (getSeatingResult.StatusCode == StatusCode.OK)
        {
            var seatingEntity = (SeatingEntity)getSeatingResult.Content!;

            var bookingEntity = new BookingEntity
            {
                BookingStartTime = model.BookingStartTime,
                BookingEndTime = model.BookingStartTime.AddHours(6),
                BookerName = model.BookerName,
                BookerEmail = model.BookerEmail,
                BookerPhone = model.BookerPhone,
                SpecialRequests = model.SpecialRequests,
                SeatingId = seatingEntity.Id,
                Seating = seatingEntity
            };

            var createResult = await _bookingRepository.CreateAsync(bookingEntity);

            if(createResult.StatusCode == StatusCode.CREATED)
                return ResponseFactory.Created(createResult);

            return ResponseFactory.BadRequest(createResult.Message!);
        }
        else if(getSeatingResult.StatusCode == StatusCode.NOT_FOUND)
            return ResponseFactory.NotFound(getSeatingResult.Message!);

        return ResponseFactory.BadRequest(getSeatingResult.Message!);
    }
}
