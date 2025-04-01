using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface ISeatingService
    {
        Task<ResponseResult> CreateCompleteSeatingAsync(SeatingCreateModel model);
        Task<ResponseResult> CreateSingleSeatingAsync(SeatingSingleModel model);
        Task<ResponseResult> GetAllSeatingsAsync(string restaurantId);
        Task<ResponseResult> GetOneSeatingAsync(string tableId);
    }
}