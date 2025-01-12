using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<RestaurantEntity> Restaurants { get; set; }
    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<ChairEntity> Chairs { get; set; }
    public DbSet<TableChairEntity> TablesChairs { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
}
