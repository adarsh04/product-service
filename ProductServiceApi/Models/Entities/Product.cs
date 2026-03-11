namespace ProductService.Models;

public class Product(Guid id, string title, string summary, decimal price)
{

    public Guid Id { get; set; } = id;
    public string Title { get; set; } = title;

    public string Summary { get; set; } = summary;

    public decimal Price { get; set; } = price;

    public DateTime? CreatedAt { get; set; }

    private Product(): this(Guid.Empty, string.Empty, string.Empty, 0) { }
}
