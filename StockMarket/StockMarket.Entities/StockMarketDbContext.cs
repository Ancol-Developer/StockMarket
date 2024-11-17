using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockMarket.Entities.IdentityEntities;

namespace StockMarket.Entities;

public class StockMarketDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public StockMarketDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
        modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
    }
}
