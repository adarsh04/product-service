using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private static readonly string[] Titles =
    [
        "apple", "banana", "carrot", "rice"
    ];

    private static readonly string[] Summaries =
    [
      "fuji red", "half ripe", "orange", "brown"  
    ];


    [HttpGet(Name = "GetProduct")]
    public IEnumerable<Product> Get()
    {
        return Enumerable.Range(0, 4).Select(index => new Product
        {
            Id = index,
            Title = Titles[index],
            Summary = Summaries[index]
        })
        .ToArray();
    }
}
