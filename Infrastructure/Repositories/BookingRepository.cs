using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BookingRepository(DataContext context) : BaseRepository<BookingEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<BookingEntity> bookingList = await _context.Bookings
                .Include(b => b.Seating)
                .ToListAsync();

            if (!bookingList.Any())
                return ResponseResult.Result(2, "List is empty.");

            return ResponseResult.Result(0, "List was found.", bookingList);

        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<BookingEntity, bool>> predicate)
    {
        try
        {
            var bookingEntity = await _context.Bookings
                .Include(b => b.Seating)
                .FirstAsync(predicate);

            if (bookingEntity == null)
                return ResponseResult.Result(2, "Entity could not be found.");

            return ResponseResult.Result(0, "Entity found.", bookingEntity);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }
}
