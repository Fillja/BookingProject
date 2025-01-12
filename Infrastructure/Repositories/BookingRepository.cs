using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class BookingRepository(DataContext context) : BaseRepository<BookingEntity>(context)
{
    private readonly DataContext _context = context;
}
