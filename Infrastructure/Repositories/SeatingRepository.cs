using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SeatingRepository(DataContext context) : BaseRepository<SeatingEntity>(context)
{
    private readonly DataContext _context = context;
}
