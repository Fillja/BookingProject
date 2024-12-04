using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class TableRepository(DataContext context) : BaseRepository<TableEntity>(context)
{
    private readonly DataContext _context = context;
}
