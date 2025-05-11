using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)    
{
    public DbSet<RestaurantEntity> Restaurants { get; set; }
    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
}
