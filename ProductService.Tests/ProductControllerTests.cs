using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using ProductService.Controllers;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace ProductService.Tests;

public class ProductControllerTests
{

    private readonly Mock<IProductRepository> _mockRepo;

    private readonly Mock<IValidator<CreateProductRequestDto>> _validator;


    private readonly ProductController _controller;

    private readonly Mock<ICacheService> _cache;


    
    public ProductControllerTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _validator = new Mock<IValidator<CreateProductRequestDto>>();

        _cache = new Mock<ICacheService>();
        _controller = new ProductController(_mockRepo.Object, _validator.Object, _cache.Object);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductIsAbsent()
    {
        var id = Guid.NewGuid();
        var result = await _controller.GetProduct(id);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetProduct_ReturnsOk_WhenProductExists()
    {
        var id = Guid.NewGuid();
        var product = new Product(id, "Test", "Summary", 1000);

        _mockRepo.Setup(r => r.FindAsync(id)).ReturnsAsync(product);
        var result = await _controller.GetProduct(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var responseDto = Assert.IsType<ProductResponseDto>(okResult.Value);
    
        Assert.Equal(id, responseDto.Id); 

    }

    [Fact]
    public async Task CreateProduct_ReturnsBadRequest_WhenPriceIsNegative()
    {
        var invalidDto = new CreateProductRequestDto { Title = "Bad Product", Summary = "Bad product summary", Price = -10.00m };
        
        var validationFailure = new FluentValidation.Results.ValidationResult(new[] 
        { 
            new FluentValidation.Results.ValidationFailure("Price", "Price cannot be negative") 
        });

        _validator.Setup(v => v.ValidateAsync(invalidDto, default))
                    .ReturnsAsync(validationFailure);


        var result = await _controller.CreateProduct(invalidDto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
