using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IChairService
    {
        Task<ResponseResult> CreateChairAsync(ChairModel chairModel);
        Task<ResponseResult> UpdateChairAsync(string id, ChairModel chairModel);
    }
}