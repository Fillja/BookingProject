using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseResult> BookTableAndChairAsync(SeatingBookingModel seatingBookingModel);
        Task<ResponseResult> CreateBookingAsync(BookingMinimalModel bookingMinimalModel, SeatingBookingModel seatingBookingModel);
        Task<ResponseResult> DeleteBookingAsync(string id);
        Task<ResponseResult> GetOneBookingAsync(string id);
        Task<ResponseResult> UpdateBookingAsync(string id, BookingMinimalModel bookingMinimalModel);
    }
}