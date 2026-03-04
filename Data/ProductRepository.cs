using Microsoft.EntityFrameworkCore;
using ProductService.Interfaces;

namespace ProductService.Data;

public class ProductRepository: IProductRepository
{
    private readonly ProductDbContext _productDbContext;
    public ProductRepository(ProductDbContext productDbContext)
    {
        this._productDbContext = productDbContext;
    }
    public void AddProduct(Product product)
    {
        _productDbContext.Products.Add(product);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _productDbContext.SaveChangesAsync() > 0;
    }

    public async Task<Product?> FindAsync(Guid id)
    {
        return await _productDbContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
         return await _productDbContext.Products.ToListAsync();
    }
}
