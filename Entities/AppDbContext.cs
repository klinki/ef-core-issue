using Microsoft.EntityFrameworkCore;

namespace ef_tpc.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Investment> Investments { get; set; }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Bond> Bonds { get; set; }

    public DbSet<InvestmentYield> Yields { get; set; }

    public DbSet<Dividend> Dividends { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var investment = modelBuilder.Entity<Investment>();
        investment.UseTpcMappingStrategy();

        modelBuilder.Entity<Stock>();
        modelBuilder.Entity<Bond>();

        modelBuilder.Entity<InvestmentYield>().UseTpcMappingStrategy();
    }
}
