using EntityFrameworkExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Data;

public class StoreContext(DbContextOptions<StoreContext> options, SoftDeleteInterceptor softDeleteInterceptor) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(softDeleteInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("store");
        modelBuilder.Entity<Product>()
            .HasMany(x => x.Sales)
            .WithMany(x => x.Products)
            .UsingEntity<ProductSale>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    }

    public DbSet<Seller> Sellers { get; set; } = default!;
    public DbSet<Sale> Sales { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductSale> ProductsSales { get; set; } = default!;
}