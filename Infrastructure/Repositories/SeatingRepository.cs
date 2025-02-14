using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatingRepository(DataContext context) : BaseRepository<SeatingEntity>(context)
{
    private readonly DataContext _context = context;

    public virtual async Task<ResponseResult> GetAllWithIdAsync(string id)
    {
        try
        {
            IEnumerable<SeatingEntity> entityList = await _context.Seatings.Where(x => x.TableId == id).ToListAsync();
            if (entityList.Any())
                return ResponseFactory.Ok(entityList);

            return ResponseFactory.NotFound("List is empty.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }
}
