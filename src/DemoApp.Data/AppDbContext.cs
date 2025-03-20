using DemoApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<InventoryItem> Inventory { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
