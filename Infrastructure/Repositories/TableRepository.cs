using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class TableRepository(DataContext context) : BaseRepository<TableEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync(string restaurantId)
    {
        try
        {
            var tableList = await _context.Tables
                .Where(t => t.RestaurantId == restaurantId)
                .Include(t => t.Bookings)
                .ToListAsync();

            if (tableList.Count.Equals(0))
                return ResponseResult.Result(2, "List is empty.");

            return ResponseResult.Result(0, "List was found.", tableList);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<TableEntity, bool>> predicate)
    {
        try
        {
            var tableEntity = await _context.Tables
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(predicate);

            if (tableEntity == null)
                return ResponseResult.Result(2, "Table could not be found.");

            return ResponseResult.Result(0, "Table was found.", tableEntity);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }
}
