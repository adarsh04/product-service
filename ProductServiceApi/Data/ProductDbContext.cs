using Microsoft.EntityFrameworkCore;
using ProductService.Models;

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
                (
                    Guid.Parse("7ed3a118-ef73-4560-8438-66275b634812"),
                    "Industrial Scanner Pro",
                    "High-speed laser scanner for physical environments.",
                    899.99m
                ),
                new Product
                (
                    Guid.Parse("3f82057d-2b3a-449e-8930-466373e20601"),
                    "Rugged Tablet X1",
                    "Drop-proof tablet for real-time inventory tracking.",
                    1249.50m
                )
        );
    }

    internal async Task<Product> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}