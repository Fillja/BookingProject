using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class OrderRepository(DataContext context) : BaseRepository<OrderEntity>(context)
{
    private readonly DataContext _context = context;
}
