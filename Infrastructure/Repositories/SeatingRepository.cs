using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatingRepository(DataContext context) : BaseRepository<SeatingEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync(string restaurantId)
    {
        try
        {
            IEnumerable<SeatingEntity> seatingList = await _context.Seatings
                .Where(s => s.Table.RestaurantId == restaurantId)
                .Include(s => s.Table)
                .Include(s => s.Chair)
                .ToListAsync();

            if (!seatingList.Any())
                return ResponseResult.Result(2, "List is empty.");

            return ResponseResult.Result(0, "List was found.", seatingList);

        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetAllWithTableIdAsync(string tableId)
    {
        try
        {
            IEnumerable<SeatingEntity> seatingList = await _context.Seatings
                .Where(s => s.TableId == tableId)
                .Include(s => s.Table)
                .Include(s => s.Chair)
                .ToListAsync();

            if (!seatingList.Any())
                return ResponseResult.Result(2, "List is empty.");

            return ResponseResult.Result(0, "List was found.", seatingList);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    
}
