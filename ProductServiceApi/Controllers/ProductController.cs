using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Mappings;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repo;
    private readonly IValidator<CreateProductRequestDto> _validator;

    public ProductController(IProductRepository repo, IValidator<CreateProductRequestDto> validator)
    {
        _repo = repo;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        var products = await _repo.GetProductsAsync();

        var productDtos = products.Select(p => p.ToDto());

        return Ok(productDtos);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _repo.FindAsync(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with ID {id} not found" });
        }

        return Ok(product.ToDto());
    }


    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductRequestDto productDto)
    {

        var validationResult = await _validator.ValidateAsync(productDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }


        var product = productDto.ToEntity(); 

        // 2. Add to Postgres and Save (this populates the GUID)
        _repo.AddProduct(product);
        await _repo.SaveChangesAsync();

        // 3. Map back to Response DTO for the final output
        var response = product.ToDto();

        // 4. Return 201 Created with the correct Location header
        return CreatedAtAction(
            nameof(GetProduct), 
            new { id = response.Id }, // Use the newly generated ID
            response
        );
    }


    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateProduct(Guid id, Product product)
    // {
    //     if (id != product.Id) return BadRequest("ID mismatch");

    //     _context.Entry(product).State = EntityState.Modified;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException)
    //     {
    //         if (!_context.Products.Any(e => e.Id == id)) return NotFound();
    //         throw;
    //     }

    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteProduct(int id)
    // {
    //     var product = await _context.Products.FindAsync(id);
    //     if (product == null) return NotFound();

    //     _context.Products.Remove(product);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }
}
