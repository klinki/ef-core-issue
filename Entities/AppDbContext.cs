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

        // TODO: This is the part of code which causes migrations to freeze.
        // To prevent freezes, remove (or comment) code below:

        var dividend = modelBuilder.Entity<Dividend>();
        dividend.Property(e => e.InvestmentId)
            .HasColumnName("stock_id");

        dividend.HasOne<Stock>()
            .WithMany(s => s.Dividends)
            .HasForeignKey(e => e.InvestmentId);

        var coupon = modelBuilder.Entity<Coupon>();
        coupon.Property(e => e.InvestmentId)
            .HasColumnName("bond_id");

        coupon.HasOne<Bond>()
            .WithMany(b => b.Coupons)
            .HasForeignKey(e => e.InvestmentId);
    }
}
