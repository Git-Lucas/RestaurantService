using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Item> Items { get; set; }
}
