using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;
using Xunit;
using ProductService.Models;
using ProductService.Services;

public class CacheServiceTests
{
    private readonly Mock<IDistributedCache> _mockCache;
    private readonly CacheService _service;


    public CacheServiceTests()
    {
        _mockCache = new Mock<IDistributedCache>();
        _service = new CacheService(_mockCache.Object);
    }



    [Fact]
    public async Task GetDataAsync_WhenKeyDoesNotExist_ReturnsDefault()
    {
        _mockCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                  .ReturnsAsync((byte[])null);

        var result = await _service.GetDataAsync<string>("missing_key");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetDataAsync_WhenKeyExists_ReturnsValue()
    {

        var expectedProduct = new { Id = Guid.NewGuid(), Title = "Test Product", Summary = "Summary"};
        var json = JsonSerializer.Serialize(expectedProduct);
        var cacheKey = "product_123";

        // IDistributedCache uses byte arrays internally
        _mockCache.Setup(x => x.GetAsync(cacheKey, default))
                  .ReturnsAsync(Encoding.UTF8.GetBytes(json));

        // Act
        var result = await _service.GetDataAsync<Product>(cacheKey);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Title);
        _mockCache.Verify(x => x.GetAsync(cacheKey, default), Times.Once);

    }
}