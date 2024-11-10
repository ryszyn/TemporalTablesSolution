namespace WebApi.Controllers;

using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/products"), ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received productDto: {productDto.Id}, {productDto.Name}, {productDto.Price}");

        var product = new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
        };

        await this.productRepository.AddAsync(product, cancellationToken);

        var name = nameof(this.AddAsync);

        return this.Created();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(id, cancellationToken);

        if (product == null)
        {
            return this.NotFound();
        }

        await this.productRepository.DeleteAsync(id, cancellationToken);

        return this.NoContent();
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await this.productRepository.GetAllAsync(cancellationToken);

        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();

        return this.Ok(productDtos);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(id, cancellationToken);

        if (product == null)
        {
            return this.NotFound();
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ValidFrom = product.ValidFrom,
            ValidTo = product.ValidTo,
        };

        return this.Ok(productDto);
    }

    // GET: api/products/{id}/history
    [HttpGet("{id}/history")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetHistoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var history = await this.productRepository.GetHistoryAsync(id, cancellationToken);

        var historyDtos = history.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();

        return this.Ok(historyDtos);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        var existingProduct = await this.productRepository.GetAsync(id, cancellationToken);

        if (existingProduct == null)
        {
            return this.NotFound();
        }

        var product = new Product
        {
            Id = id,
            Name = productDto.Name,
            Price = productDto.Price,
        };

        await this.productRepository.UpdateAsync(product, cancellationToken);

        return this.NoContent();
    }
}
