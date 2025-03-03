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
                return ResponseResult.Result(2, "List is empty.");

            return ResponseResult.Result(0, "List was found.", tableList);
        }
        catch (Exception ex) 
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }
}
