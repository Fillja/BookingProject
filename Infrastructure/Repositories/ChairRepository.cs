using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ChairRepository(DataContext context) : BaseRepository<ChairEntity>(context)
{
    private readonly DataContext _context = context;
}
