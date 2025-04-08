using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseResult> CreateBookingAsync(BookingModel bookingModel);
        Task<ResponseResult> GetAllBookingsAsync(string restaurantId);
        Task<ResponseResult> GetBookingAsync(string id);
        Task<ResponseResult> UpdateBookingAsync(string id, BookingModel bookingModel);
    }
}