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
                return ResponseFactory.NotFound("List is empty.");

            return ResponseFactory.Ok(bookingList, "List was found.");

        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
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
                return ResponseFactory.NotFound("Entity could not be found.");

            return ResponseFactory.Ok(bookingEntity, "Entity found.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }
}
