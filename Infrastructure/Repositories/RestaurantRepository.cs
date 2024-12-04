using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RestaurantRepository(DataContext context) : BaseRepository<RestaurantEntity>(context)
{
    private readonly DataContext _context = context;
}
