using Infrastructure.Helpers;

namespace Infrastructure.Interfaces
{
    public interface IRestaurantService
    {
        Task<ResponseResult> GetAllRestaurantsAsync();
        Task<ResponseResult> GetOneRestaurantAsync(string restaurantId);
    }
}