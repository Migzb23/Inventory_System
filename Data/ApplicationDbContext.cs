using Microsoft.EntityFrameworkCore;
using Inventory_System.Models;

namespace Inventory_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure InventoryItem entity
            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}
