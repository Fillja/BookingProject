using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface ITableService
    {
        Task<ResponseResult> CreateTableAsync(TableModel tableModel);
        Task<ResponseResult> UpdateTableAsync(string id, TableModel tableModel);
    }
}