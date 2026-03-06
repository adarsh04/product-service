using ProductService;
using ProductService.Models;

namespace ProductService.Mappings;

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
        (
            Guid.NewGuid(), // System generates the UUID
            dto.Title,
            dto.Summary,
            dto.Price
        );
    }
}