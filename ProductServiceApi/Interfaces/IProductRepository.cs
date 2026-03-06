namespace ProductService.Interfaces;

using ProductService.Models;

public interface IProductRepository
{
    void AddProduct(Product product);
    Task<bool> SaveChangesAsync();
    Task<Product?> FindAsync(Guid id);

    Task<IReadOnlyList<Product>> GetProductsAsync();
}