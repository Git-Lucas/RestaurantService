using Microsoft.EntityFrameworkCore;
using RestaurantService.Models;

namespace RestaurantService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
{
    public DbSet<Restaurant> Restaurants { get; set; }
}
