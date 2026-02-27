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
            new Product { Id = 1, Title = "Mechanical Keyboard", Summary = "RGB, Brown Switches"},
            new Product { Id = 2, Title = "Gaming Mouse", Summary = "16000 DPI, Wireless"},
            new Product { Id = 3, Title = "UltraWide Monitor", Summary = "34 inch, 144Hz"}
        );
    }
}