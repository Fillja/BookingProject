using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class TableChairRepository(DataContext context) : BaseRepository<TableChairEntity>(context)
{
    private readonly DataContext _context = context;
}
