namespace ProductService;

public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
