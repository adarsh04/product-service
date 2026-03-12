using Microsoft.EntityFrameworkCore;

using ProductService.Interfaces;
namespace ProductService.Data;
using ProductService.Models;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public void AddProduct(Product product)
    {
        _applicationDbContext.Products.Add(product);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _applicationDbContext.SaveChangesAsync() > 0;
    }

    public async Task<Product?> FindAsync(Guid id)
    {
        return await _applicationDbContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
         return await _applicationDbContext.Products.ToListAsync();
    }
}
