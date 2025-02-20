using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatingRepository(DataContext context) : BaseRepository<SeatingEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<SeatingEntity> seatingList = await _context.Seatings
                .Include(s => s.Table)
                .Include(s => s.Chair)
                .ToListAsync();

            if (!seatingList.Any())
                return ResponseFactory.NotFound("List is empty.");

            return ResponseFactory.Ok(seatingList, "List was found.");

        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
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

            if (seatingList.Any())
                return ResponseFactory.Ok(seatingList);

            return ResponseFactory.NotFound("List is empty.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    
}
