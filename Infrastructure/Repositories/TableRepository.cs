using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TableRepository(DataContext context) : BaseRepository<TableEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TableEntity> tableList = await _context.Tables
                .Include(t => t.Restaurant)
                .ToListAsync();

            if (!tableList.Any())
                return ResponseFactory.NotFound("List is empty.");

            return ResponseFactory.Ok(tableList, "List was found.");
        }
        catch (Exception ex) 
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }
}
