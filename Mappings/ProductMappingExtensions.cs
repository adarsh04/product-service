using ProductService;

public static class ProductMappingExtensions
{
    public static ProductResponseDto ToDto(this Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Title = product.Title,
            Summary = product.Summary,
            Price = product.Price
        };
    }

    public static Product ToEntity(this CreateProductRequestDto dto)
    {
        return new Product
        {
            Id = Guid.NewGuid(), // System generates the UUID
            Title = dto.Title,
            Summary = dto.Summary,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow // System sets the timestamp
        };
    }
}