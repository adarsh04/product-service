// Models/DTOs/ProductResponseDto.cs
namespace ProductService.Models;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
}