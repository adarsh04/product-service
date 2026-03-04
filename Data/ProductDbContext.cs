using Microsoft.EntityFrameworkCore;
using ProductService;

namespace ProductService.Data;

public class ProductDbContext : DbContext
{
    // Pass the configuration (connection string) down to the base class
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    // This represents the "Products" table in your DB
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("7ed3a118-ef73-4560-8438-66275b634812"),
                    Title = "Industrial Scanner Pro",
                    Summary = "High-speed laser scanner for physical environments.",
                    Price = 899.99m,
                    CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = Guid.Parse("3f82057d-2b3a-449e-8930-466373e20601"),
                    Title = "Rugged Tablet X1",
                    Summary = "Drop-proof tablet for real-time inventory tracking.",
                    Price = 1249.50m,
                    CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc)
                }
        );
    }

    internal async Task<Product> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}