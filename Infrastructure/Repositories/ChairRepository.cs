using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChairRepository(DataContext context) : BaseRepository<ChairEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<ChairEntity> chairList = await _context.Chairs
                .Include(c => c.Restaurant)
                .ToListAsync();

            if (!chairList.Any())
                return ResponseFactory.NotFound("List is empty.");

            return ResponseFactory.Ok(chairList, "List was found.");

        }
        catch(Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }
}
